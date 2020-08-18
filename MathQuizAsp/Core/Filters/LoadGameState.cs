using MathQuizAsp.Features.Game;
using System.Web.Mvc;
using MathQuizAsp.Controllers;
using Microsoft.Ajax.Utilities;
using System.Web.Routing;
using MathQuizAsp.ViewModels;
using System.ComponentModel.DataAnnotations;

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
                GameSettingsVM newGameConfig = session[HomeController.GAME_CONFIG] as GameSettingsVM;

                var validationCtx = new ValidationContext(newGameConfig, serviceProvider: null, items: null);
                bool isConfigValid = Validator.TryValidateObject(newGameConfig, validationCtx, null, true);
                if (!isConfigValid)
                {
                    filterContext.Result = GenerateRedirectUrl("BadRequest", "Error");
                }

                currentGameState = new GameState(newGameConfig);
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