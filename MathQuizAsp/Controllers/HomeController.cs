using MathQuizAsp.ViewModels;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MathQuizAsp.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        public const string GAME_CONFIG = "GameConfig";
        public GameSettingsVM GameConfig
        {
            get
            {
                if (Session[GAME_CONFIG] == null)
                {
                    Session[GAME_CONFIG] = new GameSettingsVM();
                }
                return Session[GAME_CONFIG] as GameSettingsVM;
            }
            set
            {
                Session[GAME_CONFIG] = value;
            }
        }

        public ActionResult Index()
        {
            Session.Clear();
            return View(GameConfig);
        }

        [HttpPost]
        public ActionResult Index(GameSettingsVM formData)
        {
            if (ModelState.IsValid)
            {
                GameConfig = formData;
                return RedirectToAction("Index", "Game");
            }
            else
            {
                return Index();
            }
        }
    }

}