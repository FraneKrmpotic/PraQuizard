using PRA.DBQuizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRA.Controllers
{
    public class QuizController : Controller
    {
        private DBQuizardEntities _context;

        public QuizController()
        {
            _context = new DBQuizardEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Quiz
        public ActionResult Index()
        {
           return View();
        }




    }
}