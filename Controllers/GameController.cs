namespace MathQuizAsp.Controllers
{
    using MathQuizAsp.Models;
    using Microsoft.Ajax.Utilities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class GameController : Controller
    {

        public ActionResult Index()
        {
            // Validation
            if (Session["ConfigDifficulty"] == null ||
                Session["ConfigDifficulty"].ToString().IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home", new { error = "Difficulty is not set!?" });
            }
            else if (Session["ConfigQuestionCount"] == null || (int)Session["ConfigQuestionCount"] < 1)
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
        public ActionResult Index(UserAnswer body)
        {
            GameViewModel vm = Session["GameViewModel"] as GameViewModel;
            
            if (vm == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return Index();
            }


            if (vm.IsAnsweringMode)
            {
                if (!body.Answer.IsNullOrWhiteSpace())
                {
                    int.TryParse(body.Answer, out int userGuess);
                    vm.CheckAnswer(userGuess);
                }
            }
            else
            {
                vm.NextQuestion();
            }

            return View(vm);
        }

        public ActionResult TimerIsUp()
        {
            GameViewModel vm = Session["GameViewModel"] as GameViewModel;
            if (vm != null)
            {
                vm.ForceFinish();
            }
            return RedirectToAction("Index", "Game");
        }
    }
}
