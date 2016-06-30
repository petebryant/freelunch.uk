using freelunch.uk.Models;
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
        private ApplicationDbContext context = new ApplicationDbContext();

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

        public ActionResult Specialists()
        {
            ViewBag.Message = "Find an specialist.";

            SpecialistsViewModel model = new SpecialistsViewModel();
            model.Specialists = context.Specialists.ToList();

            return View(model);
        }

        public ActionResult Privacy()
        {
            ViewBag.Message = "Privacy Statement";

            return View();
        }

        public ActionResult Terms()
        {
            ViewBag.Message = "Terms and Conditions";

            return View();
        }
    }
}