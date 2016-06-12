using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace freelunch.uk.Controllers
{
    [RedirectOnError]
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Lunch()
        {
            ViewBag.Message = "Arrange a lunch.";

            return View();
        }

        public ActionResult Specialist()
        {
            ViewBag.Message = "Find an specialist.";

            return View();
        }

        public ActionResult Privacy()
        {
            ViewBag.Message = "Privacy Statement";

            return View();
        }
    }
}