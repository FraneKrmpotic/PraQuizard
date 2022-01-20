using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRA.DBQuizard;

namespace PRA.Controllers
{
    public class AnswersController : Controller
    {
        private DBQuizardEntities db = new DBQuizardEntities();

        // GET: Answers
        public ActionResult Index(int? id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }


            HttpCookie kuki03 = new HttpCookie("question");
            kuki03["ID"] = db.Question.FirstOrDefault(x => x.IDQuestion == id).IDQuestion.ToString();
            kuki03.Expires = DateTime.Now.AddSeconds(500); //povecati trajanje kuki-a
            Response.Cookies.Add(kuki03);

            ViewBag.IDQuestion = id;

            var answer = db.Answer.Include(a => a.Question);
            return View(answer.ToList());
        }

        // GET: Answers/Details/5
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
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            ViewBag.QuestionID = new SelectList(db.Question, "IDQuestion", "Question1");
            ViewBag.questionId = Request.Cookies["question"]["id"].ToString();
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAnswer,Answer1,RightAnswer,IsActive,QuestionID")] Answer answer)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.Answer.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Request.Cookies["quiz"]["ID"].ToString() });
            }

            ViewBag.QuestionID = new SelectList(db.Question, "IDQuestion", "Question1", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Edit/5
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
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Question, "IDQuestion", "Question1", answer.QuestionID);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAnswer,Answer1,RightAnswer,IsActive,QuestionID")] Answer answer)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Request.Cookies["question"]["ID"].ToString() });
            }
            ViewBag.QuestionID = new SelectList(db.Question, "IDQuestion", "Question1", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Delete/5
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
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            Answer answer = db.Answer.Find(id);
            db.Answer.Remove(answer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
