﻿using System;
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
        public ActionResult Index()
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
