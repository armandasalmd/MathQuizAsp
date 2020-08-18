using MathQuizAsp.Models;
using MathQuizCore.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MathQuizAsp.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        public const string SESSION_DIFF = "ConfigDifficulty";
        public const string SESSION_QCOUNT = "ConfigQuestionCount";

        public ActionResult Index()
        {
            Session.Clear();
            return View(new GameSettings());
        }

        [HttpPost]
        public ActionResult Index(GameSettings gameConfig)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Session[SESSION_DIFF] = gameConfig.Difficulty;
                    Session[SESSION_QCOUNT] = int.Parse(gameConfig.QuestionCount);
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