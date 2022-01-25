using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRA.DBQuizard;
using PRA.Dto;

namespace PRA.Controllers
{
    public class QuestionsController : Controller
    {
        private DBQuizardEntities db = new DBQuizardEntities();

        // GET: Questions
        public ActionResult Index(int? id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            HttpCookie kuki02 = new HttpCookie("quiz");
            kuki02["ID"] = db.Quiz.FirstOrDefault(x => x.IDQuiz == id).IDQuiz.ToString();
            kuki02.Expires = DateTime.Now.AddSeconds(500); //povecati trajanje kuki-a
            Response.Cookies.Add(kuki02);

            ViewBag.IDQuiz = id;
            var question = db.Question.Include(q => q.Quiz);
            return View(question.ToList());
        }
        /*
        public class ViewModel
{
    // since the CustID is numeric, I prefer using 'int' propertypublic int IsSelected { get; set; }

    public List<Customer> Customers { get; set; }

    // other properties
}
      

        [HttpPost]
        public ActionResult AddCustomerLinkToDB(AnswerDto answer)
        {
            int selectedCustomer = (int)answer.RightAnswer;
            // other stuffreturn View();
        }
          */


        // POST: Answers
        [HttpPost]
        public ActionResult Save(int id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            return RedirectToAction("Index", new { id = Request.Cookies["question"]["ID"].ToString() });

        }

        // GET: Questions/Details/5
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
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            ViewBag.QuizID = new SelectList(db.Quiz, "IDQuiz", "Title");
            ViewBag.quizId = Request.Cookies["quiz"]["id"].ToString();
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDQuestion,Question1,Duration,IsActive,QuizID")] Question question)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.Question.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Request.Cookies["quiz"]["ID"].ToString() });
            }

            ViewBag.QuizID = new SelectList(db.Quiz, "IDQuiz", "Title", question.QuizID);
            return View(question);
        }

        // GET: Questions/Edit/5
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
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizID = new SelectList(db.Quiz, "IDQuiz", "Title", question.QuizID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDQuestion,Question1,Duration,IsActive,QuizID")] Question question)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Request.Cookies["quiz"]["ID"].ToString() });
            }
            ViewBag.QuizID = new SelectList(db.Quiz, "IDQuiz", "Title", question.QuizID);
            return View(question);
        }

        // GET: Questions/Delete/5
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
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            Question question = db.Question.Find(id);
            db.Question.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Request.Cookies["quiz"]["ID"].ToString() });
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
