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
    [RedirectOnError]
    [Authorize]
    public class ExpertController : Controller
    {
        public enum ExpertMessageId
        {
            UpdateSuccess,
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
        // GET: Expert
        public ActionResult Index(ExpertMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ExpertMessageId.Error ? "An error has occurred."
                : message == ExpertMessageId.UpdateSuccess ? "You details have been updated."
                : "";

            var userId = User.Identity.GetUserId();
            var expert = context.Experts.FirstOrDefault(x => x.UserId == userId);
            var model = new ExpertViewModel();
            model.UserId = expert.UserId;

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
        public async Task<ActionResult> Create(ExpertViewModel model)
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

                Expert expert = new Expert();
                expert.UserId = userId;
                expert.Name = model.Name;
                expert.Description = model.Description;

                context.Experts.Add(expert);
                context.SaveChanges();

                return RedirectToAction("Index", new { Message = ExpertMessageId.UpdateSuccess });
            }
            catch
            {
                return View();
            }
        }

        // GET: Expert/Edit/5
        public ActionResult Edit(string id)
        {
            var expert = context.Experts.Include("Links").FirstOrDefault(x => x.UserId == id);
            var model = new ExpertViewModel();

            if (expert == null)
            {
                return View("Error");
            }

            Link link = new Link();
            link.Id = "1";
            link.Text = "hello...";
            link.URL = "go here...";
            link.LinkType = LinkType.Twitter;
            expert.Links.Add(link);

            Link link2 = new Link();
            link2.Id = "2";
            link2.Text = "blog...";
            link2.URL = "or there...";
            link2.LinkType = LinkType.Blog;
            expert.Links.Add(link2);

            model.UserId = expert.UserId;
            model.Name = expert.Name;
            model.Description = expert.Description;
            model.Links = expert.Links;

            return View(model);
        }

        // POST: Expert/Edit/5
        [HttpPost]
        public ActionResult Edit(ExpertViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", model);
                }

                var expert = context.Experts.FirstOrDefault(x => x.UserId == model.UserId);

                if (expert == null)
                {
                    return View("Error");
                }

                expert.Name = model.Name;
                expert.Description = model.Description;

                context.SaveChanges();

                return RedirectToAction("Index", new { Message = ExpertMessageId.UpdateSuccess });
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
