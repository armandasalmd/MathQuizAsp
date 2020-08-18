using MathQuizAsp.Core.Exceptions;
using MathQuizAsp.Core.Filters;
using MathQuizAsp.Features.Game;
using MathQuizAsp.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MathQuizAsp.Controllers
{
    [LoadGameState]
    [SessionState(SessionStateBehavior.Default)]
    public class GameController : Controller
    {
        public const string GAME_STATE = "GameState";
        private GameState Game
        {
            get
            {
                return Session[GAME_STATE] as GameState;
            }
        }
        
        public ActionResult Index()
        {
            return View(Game);
        }

        [HttpPost]
        public ActionResult Index(UserAnswer body)
        {
            if (Game.IsAnsweringInProgress)
            {
                if (!body.Answer.IsNullOrWhiteSpace())
                {
                    int.TryParse(body.Answer, out int userGuess);
                    try
                    {
                        Game.CheckAnswer(userGuess);
                    }
                    catch (TimerIsUpException)
                    {
                        return RedirectToAction("TimerIsUp", "Game");
                    }
                }
            }
            else
            {
                ModelState.Clear();
                Game.NextQuestion();
            }
            return View(Game);
        }

        public ActionResult TimerIsUp()
        {
            Game.ForceFinish();
            return RedirectToAction("Index", "Game");
        }
    }
}
