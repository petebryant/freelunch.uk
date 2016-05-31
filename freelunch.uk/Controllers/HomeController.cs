using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace freelunch.uk.Controllers
{
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

        public ActionResult Expert()
        {
            ViewBag.Message = "Find an expert.";

            return View();
        }

        public ActionResult Privacy()
        {
            ViewBag.Message = "Privacy Statement";

            return View();
        }
    }
}