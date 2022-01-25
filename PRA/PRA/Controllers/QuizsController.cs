using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRA.DBQuizard;
using PRA.ViewModels;

namespace PRA.Controllers
{
    public class QuizsController : Controller
    {
        private DBQuizardEntities db = new DBQuizardEntities();

        // GET: Quizs
        public ActionResult Index()
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }
            //string email = Session["userEmail"].ToString();
            //UserAcc user = db.UserAcc.SingleOrDefault(u => u.Email == email);
            //IList<Quiz> quizz = db.Quiz.Where(q => q.UserAccID == user.IDUserAcc).ToList();
            //UserIKvizovi quiz = new UserIKvizovi
            //{
            //    User = user,
            //    Quizovi = quizz
            //};
            //return View(userIKvizovi);

            var quiz = db.Quiz.Include(q => q.UserAcc);
                return View(quiz.ToList());
        }

        // GET: Quizs/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: Quizs/Create
        public ActionResult Create()
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            ViewBag.UserAccID = new SelectList(db.UserAcc, "IDUserAcc", "Email");
            ViewBag.Email = Request.Cookies["account"]["id"].ToString();
            //ViewBag.UserAccID = db.UserAcc.Where(db.UserAcc.g == Request.Cookies["account"]["ID"].ToString());
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDQuiz,Title,IsActive,UserAccID")] Quiz quiz)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.Quiz.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserAccID = new SelectList(db.UserAcc, "IDUserAcc", "Email", quiz.UserAccID);
            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserAccID = new SelectList(db.UserAcc, "IDUserAcc", "Email", quiz.UserAccID);
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDQuiz,Title,IsActive,UserAccID")] Quiz quiz)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserAccID = new SelectList(db.UserAcc, "IDUserAcc", "Email", quiz.UserAccID);
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            Quiz quiz = db.Quiz.Find(id);
            db.Quiz.Remove(quiz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Start(int id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            Quiz quiz = db.Quiz.Find(id);


            return View(quiz);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
