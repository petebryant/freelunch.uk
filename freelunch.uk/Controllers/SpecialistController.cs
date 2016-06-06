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
    public class SpecialistController : Controller
    {
        public enum SpecialistMessageId
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
        // GET: Specialist
        public ActionResult Index(SpecialistMessageId? message)
        {
            ViewBag.StatusMessage =
                message == SpecialistMessageId.Error ? "An error has occurred."
                : message == SpecialistMessageId.UpdateSuccess ? "You details have been updated."
                : "";

            var userId = User.Identity.GetUserId();
            var specialist = context.Specialist.FirstOrDefault(x => x.UserId == userId);
            var model = new SpecialistViewModel();
            model.UserId = specialist.UserId;

            Link link = new Link();
            link.Id = "1";
            link.Text = "hello...";
            link.URL = "go here...";
            link.LinkType = LinkType.Twitter;
            specialist.Links.Add(link);

            Link link2 = new Link();
            link2.Id = "2";
            link2.Text = "blog...";
            link2.URL = "or there...";
            link2.LinkType = LinkType.Blog;
            specialist.Links.Add(link2);

            if (specialist != null)
            {
                model.UserId = specialist.UserId;
                model.Name = specialist.Name;
                model.Description = specialist.Description;
                model.Links = specialist.Links;
            }

            return View(model);
        }

        // GET: Specialist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Specialist/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Specialist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpecialistViewModel model)
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

                Specialist specialist = new Specialist();
                specialist.UserId = userId;
                specialist.Name = model.Name;
                specialist.Description = model.Description;

                context.Specialist.Add(specialist);
                context.SaveChanges();

                return RedirectToAction("Index", new { Message = SpecialistMessageId.UpdateSuccess });
            }
            catch
            {
                return View();
            }
        }

        // GET: Specialist/Edit/5
        public ActionResult Edit(string id)
        {
            var specialist = context.Specialist.Include("Links").FirstOrDefault(x => x.UserId == id);
            var model = new SpecialistViewModel();

            if (specialist == null)
            {
                return View("Error");
            }

            Link link = new Link();
            link.Id = "1";
            link.Text = "hello...";
            link.URL = @"http://www.google.com";
            link.LinkType = LinkType.Twitter;
            specialist.Links.Add(link);

            Link link2 = new Link();
            link2.Id = "2";
            link2.Text = "blog...";
            link2.URL = "or there...";
            link2.LinkType = LinkType.Blog;
            specialist.Links.Add(link2);

            model.UserId = specialist.UserId;
            model.Name = specialist.Name;
            model.Description = specialist.Description;
            model.Links = specialist.Links;

            return View(model);
        }

        // POST: Specialist/Edit/5
        [HttpPost]
        public ActionResult Edit(SpecialistViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", model);
                }

                var specialist = context.Specialist.FirstOrDefault(x => x.UserId == model.UserId);

                if (specialist == null)
                {
                    return View("Error");
                }

                specialist.Name = model.Name;
                specialist.Description = model.Description;

                context.SaveChanges();

                return RedirectToAction("Index", new { Message = SpecialistMessageId.UpdateSuccess });
            }
            catch
            {
                return View();
            }
        }

        // GET: Specialist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Specialist/Delete/5
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

        [HttpPost]
        public ActionResult EditLink(string id, FormCollection collection )
        {
            try
            {
                var link = context.Links.FirstOrDefault(x => x.Id == id);

                if (link == null) return View("Error");

                link.Text = collection["Text"];
                link.URL = collection["URL"];
                link.LinkType = (LinkType)Enum.Parse(typeof(LinkType), collection["LinkType"]);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteLink(string id)
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
