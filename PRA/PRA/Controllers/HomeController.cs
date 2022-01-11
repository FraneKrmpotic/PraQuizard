using PRA.DBQuizard;
using PRA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRA.Controllers
{
    public class HomeController : Controller
    {
        private DBQuizardEntities _context;

        public HomeController()
        {
            _context = new DBQuizardEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PrijavljeniKorisnik()
        {
            string email = Session["userEmail"].ToString();
            UserAcc user = _context.UserAcc.SingleOrDefault(u => u.Email == email);
            IList<Quiz> quizzes = _context.Quiz.Where(q => q.UserAccID == user.IDUserAcc).ToList();
            UserIKvizovi userIKvizovi = new UserIKvizovi
            {
                User = user,
                Quizovi = quizzes
            };
            return View(userIKvizovi);

        }
    }
}