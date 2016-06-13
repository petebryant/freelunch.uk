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
    public class SpecialistController : Controller
    {
        public enum SpecialistMessageId
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
        public ActionResult Index(SpecialistMessageId? message)
        {
            ViewBag.StatusMessage =
                message == SpecialistMessageId.Error ? "An error has occurred."
                : message == SpecialistMessageId.DeleteSuccess ? "The record has been deleted."
                : message == SpecialistMessageId.AddSucess ? "The record has been created."
                : message == SpecialistMessageId.UpdateSuccess ? "You details have been updated."
                : "";

            var userId = User.Identity.GetUserId();
            var specialist = context.Specialist.FirstOrDefault(x => x.UserId == userId);
            var model = new SpecialistViewModel();

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


        // POST: Specialist/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SpecialistViewModel model)
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


                var specialist = context.Specialist.FirstOrDefault(x => x.UserId == userId);

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

        // POST: Specialist/Delete
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

                var specialist = context.Specialist.FirstOrDefault(x => x.UserId == userId);

                if (specialist == null)
                {
                    return View("Error");
                }

                int count = specialist.Links.Count - 1;
                for(int i=count; i >= 0; i--)
                {
                    Link link = specialist.Links.ElementAt(i);
                    specialist.Links.Remove(link);
                    context.Links.Remove(link);
                }

                context.Specialist.Attach(specialist);
                context.Specialist.Remove(specialist);

                string result = ValidationHelper.GetValidationResults(specialist);

                if (!string.IsNullOrEmpty(result)) return View("Error"); 

                context.SaveChanges();

                return RedirectToAction("Index", new { Message = SpecialistMessageId.DeleteSuccess });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLink(string id, FormCollection collection )
        {
            try
            {
                var link = context.Links.FirstOrDefault(x => x.Id == id);

                if (link == null) return View("Error");

                link.Text = collection["Text"];
                link.URL = collection["URL"];
                link.LinkType = (LinkType)Enum.Parse(typeof(LinkType), collection["LinkType"]);

                context.SaveChanges();

                return RedirectToAction("Index", new { Message = SpecialistMessageId.UpdateSuccess });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLink(string id)
        {
            try
            {
                var link = context.Links.FirstOrDefault(x => x.Id == id);

                if (link == null) return View("Error");

                context.Links.Remove(link);
                context.SaveChanges();

                return RedirectToAction("Index", new { Message = SpecialistMessageId.DeleteSuccess });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLink(FormCollection collection)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return View("Error");
                }

                var specialist = context.Specialist.FirstOrDefault(x => x.UserId == userId);

                if (specialist == null) return View("Error");

                Link link = new Link();
                link.Text = collection["DummyLink.Text"];
                link.URL = collection["DummyLink.URL"];
                link.LinkType = (LinkType)Enum.Parse(typeof(LinkType), collection["DummyLink.LinkType"]);
                link.Specialist = specialist;

                context.Specialist.Attach(specialist);
                context.Links.Add(link);

                string result = ValidationHelper.GetValidationResults(link);

                if (!string.IsNullOrEmpty(result)) return View("Error"); 
                
                context.SaveChanges();

                return RedirectToAction("Index", new { Message = SpecialistMessageId.AddSucess });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubject(FormCollection collection)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return View("Error");
                }

                var specialist = context.Specialist.FirstOrDefault(x => x.UserId == userId);

                if (specialist == null) return View("Error");

                Specialism specialism = new Specialism();
                specialism.Subject = collection["DummySpecialism.Subject"];
                specialism.Description = collection["DummySpecialism.Description"];
                specialism.Specialist = specialist;

                context.Specialist.Attach(specialist);
                context.Specialisms.Add(specialism);

                string result = ValidationHelper.GetValidationResults(specialism);

                if (!string.IsNullOrEmpty(result)) return View("Error");

                context.SaveChanges();

                return RedirectToAction("Index", new { Message = SpecialistMessageId.AddSucess });
            }
            catch
            {
                return View();
            }
        }
    }
}
