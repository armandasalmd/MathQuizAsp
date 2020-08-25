//using Microsoft.AspNetCore.Mvc.Filters;

//namespace MathQuizAspKnockout.Core.Filters
//{
//    public class LoadGameState : FilterAttribute, IActionFilter
//    {
//        public void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            return;
//        }

//        public void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            var session = filterContext.HttpContext.Session;
//            var currentGameState = session[GameController.GAME_STATE];

//            if (currentGameState == null ||
//                currentGameState.GetType() != typeof(GameState))
//            {
//                GameSettingsVM newGameConfig = session[HomeController.GAME_CONFIG] as GameSettingsVM;
//                bool isConfigValid;
//                try
//                {
//                    var validationCtx = new ValidationContext(newGameConfig, serviceProvider: null, items: null);
//                    isConfigValid = Validator.TryValidateObject(newGameConfig, validationCtx, null, true);
//                }
//                catch
//                {
//                    filterContext.Result = GenerateRedirectUrl("BadRequest", "Error");
//                    return;
//                }
//                if (!isConfigValid)
//                {
//                    filterContext.Result = GenerateRedirectUrl("BadRequest", "Error");
//                    return;
//                }

//                currentGameState = new GameState(newGameConfig);
//                session[GameController.GAME_STATE] = currentGameState;
//            }
//        }

//        private RedirectToRouteResult GenerateRedirectUrl(string action, string controller)
//        {
//            return new RedirectToRouteResult(new RouteValueDictionary(
//                new { action, controller }
//            ));
//        }
//    }
//}
