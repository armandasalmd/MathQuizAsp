using MathQuizAsp.GameCore;
using MathQuizAsp.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MathQuizAsp.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            // Validation
            if (Session["ConfigDifficulty"] == null || Session["ConfigDifficulty"].ToString().IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home", new { error = "Difficulty is not set!?" });
            } else if (Session["ConfigQuestionCount"] == null || (int)Session["ConfigQuestionCount"] < 1)
            {
                return RedirectToAction("Index", "Home", new { error = "Question count is not set!?" });
            }

            GameViewModel vm;
            if (Session["GameViewModel"] == null)
            {
                string difficulty = (string)Session["ConfigDifficulty"];
                int qcount = (int)Session["ConfigQuestionCount"];
                vm = new GameViewModel(difficulty, qcount);
                Session["GameViewModel"] = vm;
            }
            else
            {
                // INFO: now if I edit 'vm' Session["GameViewModel"] 
                // also edits because it is variable by reference
                vm = Session["GameViewModel"] as GameViewModel;
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(UserAnswerDTO body)
        {
            GameViewModel vm = Session["GameViewModel"] as GameViewModel;
            if (vm.IsAnsweringMode)
            {
                if (!body.UserAnswer.IsNullOrWhiteSpace())
                {
                    int.TryParse(body.UserAnswer, out int userGuess);
                    vm.CheckAnswer(userGuess);
                }
            }
            else
            {
                // Go to the next question
                vm.NextQuestion();
            }
            
            return View(vm);
        }
    }
}