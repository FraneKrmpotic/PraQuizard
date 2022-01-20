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
    public class UserAccsController : Controller
    {
        private DBQuizardEntities db = new DBQuizardEntities();

        // GET: UserAccs
        public ActionResult Index()
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            return View(db.UserAcc.ToList());
        }

        // GET: UserAccs/Details/5
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
            UserAcc userAcc = db.UserAcc.Find(id);
            if (userAcc == null)
            {
                return HttpNotFound();
            }
            return View(userAcc);
        }

        // GET: UserAccs/Create
        public ActionResult Create()
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            return View();
        }

        // POST: UserAccs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDUserAcc,Email,Pass,Username,IsActive")] UserAcc userAcc)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.UserAcc.Add(userAcc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userAcc);
        }

        // GET: UserAccs/Edit/5
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
            UserAcc userAcc = db.UserAcc.Find(id);
            if (userAcc == null)
            {
                return HttpNotFound();
            }
            return View(userAcc);
        }

        // POST: UserAccs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUserAcc,Email,Pass,Username,IsActive")] UserAcc userAcc)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            if (ModelState.IsValid)
            {
                db.Entry(userAcc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAcc);
        }

        // GET: UserAccs/Delete/5
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
            UserAcc userAcc = db.UserAcc.Find(id);
            if (userAcc == null)
            {
                return HttpNotFound();
            }
            return View(userAcc);
        }

        // POST: UserAccs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.Cookies["account"] == null)
            {
                return Redirect("~/WEBFORME/PrijavaPostojecegKorisnika.aspx");
            }

            UserAcc userAcc = db.UserAcc.Find(id);
            db.UserAcc.Remove(userAcc);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("~/ WEBFORME / PrijavaPostojecegKorisnika.aspx");
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
