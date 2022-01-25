using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRA.Controllers
{
    public class JoinQuizController : Controller
    {
        // GET: JoinQuiz
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Join()
        {
            return View();
        }
    }
}