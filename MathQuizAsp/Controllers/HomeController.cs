using MathQuizAsp.Models;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MathQuizAsp.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        public const string GAME_CONFIG = "GameConfig";
        public GameSettings GameConfig
        {
            get
            {
                if (Session[GAME_CONFIG] == null)
                {
                    Session[GAME_CONFIG] = new GameSettings();
                }
                return Session[GAME_CONFIG] as GameSettings;
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
        public ActionResult Index(GameSettings formData)
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