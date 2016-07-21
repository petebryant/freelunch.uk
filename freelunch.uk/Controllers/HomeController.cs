using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using freelunch.uk.Models;
using freelunch.uk.Common;

namespace freelunch.uk.Controllers
{
    [RedirectOnError]
    [RequireHttps]
    public class HomeController : Controller
    {
        // TODO do not show you need to be registered message if no specialists on system
        // TODO do not show you need to be registered message if no lunches on system
        // TODO add message when there are no lunches or specialists - good for search results.
        // TODO put back contact email address on _Layout page once on has been set up to reccieve
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

        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchSpecialists(string search)
        {
            SpecialistsViewModel model = GetSearchSpecialistsResult(search);

            return View("Specialists", model);
        }

        private SpecialistsViewModel GetSearchSpecialistsResult(string search)
        {
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
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ContactSpecialist(SpecialistsViewModel vm)
        {
            // this is a honey pot to stop spambots
            if (string.IsNullOrEmpty(vm.URL))
            {
                var user = await UserManager.FindByIdAsync(vm.UserId);

                if (user == null)
                {
                    return View("Error");
                }

                string recipient = user.Email;

                //TODO need to format the contact email message
                string message = "This is a contact message from " + vm.Email;
                message += " " + vm.Sender + " would like to arrange a lunch.";
                message += " " + vm.Message;

                await Functions.SendGridAsync(recipient, message);
                ViewBag.StatusMessage = "Contact email was successfully sent";
            }

            SpecialistsViewModel model = GetSearchSpecialistsResult(vm.Search);

            return View("Specialists", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ContactLunch(LunchesViewModel vm)
        {
            // this is a honey pot to stop spambots
            if (string.IsNullOrEmpty(vm.URL))
            {
                var user = await UserManager.FindByIdAsync(vm.UserId);

                if (user == null)
                {
                    return View("Error");
                }

                string recipient = user.Email;

                //TODO need to format the contact email message
                string message = "This is a contact message from " + vm.Email;
                message += " " + vm.Sender + " would like to arrange a lunch.";
                message += " " + vm.Message;

                await Functions.SendGridAsync(recipient, message);
                ViewBag.StatusMessage = "Contact email was successfully sent";
            }

            LunchesViewModel model = GetSearchLunchResult(vm.Search);

            return View("Lunches", model);
        }



        public ActionResult Specialists()
        {
            SpecialistsViewModel model = new SpecialistsViewModel();
            model.Specialists = context.Specialists.ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchLunch(string search)
        {
            LunchesViewModel model = GetSearchLunchResult(search);

            return View("Lunch", model);
        }

        private LunchesViewModel GetSearchLunchResult(string search)
        {
            LunchesViewModel model = new LunchesViewModel();

            if (string.IsNullOrEmpty(search))
            {
                model.Lunches = context.Lunches.ToList();
            }
            else
            {
                model.Lunches = context.Lunches.Where(s => s.Name.Contains(search)
                                                                    || s.ContactName.Contains(search)
                                                                    || s.Email.Contains(search)).ToList();
            }

            return model;
        }

        public ActionResult Lunch()
        {
            LunchesViewModel model = new LunchesViewModel();
            model.Lunches = context.Lunches.ToList();

            return View(model);
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }
    }
}