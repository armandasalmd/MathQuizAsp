using MathQuizAsp.GameCore;
using MathQuizAsp.Models;
using System;
using System.Linq;
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

            ViewBag.DifficultiesList = Enum.GetNames(typeof(DifficultyLevel)).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(GameSettings gameConfig)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Session["ConfigDifficulty"] = gameConfig.Difficulty;
                    Session["ConfigQuestionCount"] = int.Parse(gameConfig.QuestionCount);
                    return RedirectToAction("Index", "Game");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home", new { error = "Invalid options selected" });
                }
            }
            else
            {
                return Index();
            }
        }
    }

}