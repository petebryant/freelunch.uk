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

        public ActionResult Lunch()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string search)
        {
            SpecialistsViewModel model = GetSearchResult(search);

            return View("Specialists", model);
        }

        private SpecialistsViewModel GetSearchResult(string search)
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
        public async Task<ActionResult> Contact(SpecialistsViewModel vm)
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

            SpecialistsViewModel model = GetSearchResult(vm.Search);

            return View("Specialists", model);
        }



        public ActionResult Specialists()
        {
            SpecialistsViewModel model = new SpecialistsViewModel();
            model.Specialists = context.Specialists.ToList();

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