using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using freelunch.uk.Models;

namespace freelunch.uk.Controllers
{
    [Authorize]
    public class ExpertController : Controller
    {
        public enum ExpertMessageId
        {
            UpdateSuccess,
            Error
        }

        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Expert
        public ActionResult Index(ExpertMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ExpertMessageId.Error ? "An error has occurred."
                : message == ExpertMessageId.UpdateSuccess ? "You details have been updated."
                : "";

            var userId = User.Identity.GetUserId();
            var expert = context.Experts.FirstOrDefault(x => x.User.Id == userId);
            var model = new ExpertViewModel();
            model.Id = userId;

            if (expert != null)
            {
                model.Name = expert.Name;
                model.Description = expert.Description;
            }

            return View(model);
        }

        // GET: Expert/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expert/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expert/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpertViewModel model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Expert/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Expert/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Expert/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Expert/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
