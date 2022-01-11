using PRA.DBQuizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;

namespace PRA.Controllers
{
    public class UserController : Controller
    {
        private DBQuizardEntities _context;

        public UserController()
        {
            _context = new DBQuizardEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


    }
}