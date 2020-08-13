using MathQuizAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MathQuizAsp.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session.Count > 0)
            {
                Session.Clear();
            }
            
            List<string> difficulties = new List<string>() { "Easy", "Medium", "Hard" };
            ViewBag.DifficultiesList = difficulties;

            

            return View();
        }

        [HttpPost]
        public ActionResult Index(GameSettings gameConfig)
        {
            try
            {
                Session["ConfigDifficulty"] = gameConfig.difficulty;
                Session["ConfigQuestionCount"] = int.Parse(gameConfig.qcount);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid options selected" });
            }
            
            return RedirectToAction("Index", "Game");
        }
    }
}