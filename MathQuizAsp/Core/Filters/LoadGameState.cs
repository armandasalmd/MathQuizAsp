using MathQuizAsp.Features.Game;
using System.Web.Mvc;
using MathQuizAsp.Controllers;
using Microsoft.Ajax.Utilities;
using System.Web.Routing;

namespace MathQuizAsp.Core.Filters
{
    public class LoadGameState : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var currentGameState = session[GameController.GAME_STATE];

            if (currentGameState == null || 
                currentGameState.GetType() != typeof(GameState))
            {
                string newStateDifficulty = (string)session[HomeController.SESSION_DIFF];
                int newStateQCount = (int)session[HomeController.SESSION_QCOUNT];

                if (newStateDifficulty.IsNullOrWhiteSpace() || newStateQCount < 1)
                {
                    filterContext.Result = GenerateRedirectUrl("BadRequest", "Error");
                }
                currentGameState = new GameState(newStateDifficulty, newStateQCount);
                session[GameController.GAME_STATE] = currentGameState;
            }
        }

        private RedirectToRouteResult GenerateRedirectUrl(string action, string controller)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(
                new { action, controller }
            ));
        }
    }
}