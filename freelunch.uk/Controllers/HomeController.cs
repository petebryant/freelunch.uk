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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string search)
        {
            ViewBag.Message = "Find an specialist.";

            SpecialistsViewModel model = new SpecialistsViewModel();

            if (string.IsNullOrEmpty(search))
            {
                model.Specialists = context.Specialists.ToList();
            }
            else
            {
                model.Specialists = context.Specialists.Where(s => s.Name.Contains(search)
                                                                    || s.Description.Contains(search)
                                                                    || s.Specialisms.Any(l => l.Subject.Contains(search))
                                                                    || s.Specialisms.Any(l => l.Description.Contains(search))
                                                                    || s.Links.Any(l => l.Text.Contains(search))
                                                                    || s.Links.Any(l => l.URL.Contains(search))).ToList();
            }

            return View("Specialists", model);
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