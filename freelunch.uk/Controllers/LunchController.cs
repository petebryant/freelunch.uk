using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using freelunch.uk.Models;
using System.Data.Entity;
using freelunch.uk.Common;

namespace freelunch.uk.Controllers
{
    [RedirectOnError]
    [RequireHttps]
    [Authorize]
    public class LunchController : Controller
    {
        public enum LunchMessageId
        {
            UpdateSuccess,
            DeleteSuccess,
            AddSucess,
            Error
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Specialist
        public ActionResult Index(LunchMessageId? message, int tab = 0)
        {
            ViewBag.StatusMessage =
                message == LunchMessageId.Error ? "An error has occurred."
                : message == LunchMessageId.DeleteSuccess ? "The record has been deleted."
                : message == LunchMessageId.AddSucess ? "The record has been created."
                : message == LunchMessageId.UpdateSuccess ? "The record have been updated."
                : "";

            ViewBag.Tab = tab;
            var userId = User.Identity.GetUserId();
            var lunch = context.Lunches.FirstOrDefault(x => x.UserId == userId);
            var model = new LunchViewModel();

            if (lunch != null)
            {
                model.UserId = lunch.UserId;
                model.Name = lunch.Name;
                model.ContactName = lunch.ContactName;
                model.Email = lunch.Email;
                model.Website = lunch.Website;
                model.Image = lunch.Image;
                model.Audience = lunch.Audience;
                model.AudienceNumber = lunch.AudienceNumber;

                foreach (var topic in lunch.Topics)
                {
                    if (string.IsNullOrEmpty(model.DisplayTopic.Name))
                        model.DisplayTopic.Name = topic.Name;
                    else
                        model.DisplayTopic.Name += ", " + topic.Name;
                }
                model.DisplayTopic.Name = "SOLID, C#, Clean Code";

                //create an array of distinct topic names for type ahead
                ViewBag.Topics = context.Topics.Select(x => x.Name).ToList<string>().Distinct(StringComparer.InvariantCultureIgnoreCase).ToArray();
            }

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LunchViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", model);
                }

                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return View("Error");
                }

                Lunch lunch = new Lunch();
                lunch.UserId = userId;
                lunch.Name = model.Name;
                lunch.Website = model.Website;
                lunch.ContactName = model.ContactName;
                lunch.Email = model.Email;
                lunch.Image = model.Image;
                lunch.Audience = model.Audience;
                lunch.AudienceNumber = model.AudienceNumber;

                context.Lunches.Add(lunch);
                context.SaveChanges();

                return RedirectToAction("Index", new { Message = LunchMessageId.AddSucess });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return View("Error");
                }

                var lunch = context.Lunches.FirstOrDefault(x => x.UserId == userId);

                if (lunch == null) return View("Error");

                context.Lunches.Remove(lunch);
                context.SaveChanges();

                return RedirectToAction("Index", new { Message = LunchMessageId.DeleteSuccess });
            }
            catch
            {
                return View();
            }
        }

    }
}