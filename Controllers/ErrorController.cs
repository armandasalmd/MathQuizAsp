using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MathQuizAsp.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
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