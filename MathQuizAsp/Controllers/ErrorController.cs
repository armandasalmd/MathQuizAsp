using System.Web.Mvc;

namespace MathQuizAsp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return NotFound();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }

        public ActionResult InternalError() 
        {
            return View();
        }
    }
}