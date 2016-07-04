using freelunch.uk.Models;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
                //TODO get specilaist email address...
                await ConfigSendGridAsync("pete.bryant@gmail.com", vm.Sender, vm.Email, vm.Subject);   
            }

            SpecialistsViewModel model = GetSearchResult(vm.Search);


            //TODO provide success or fail message
            return View("Specialists", model);
        }

        private async Task ConfigSendGridAsync(string to, string sender, string email, string subject)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(to);
            myMessage.From = new System.Net.Mail.MailAddress(
                                email, sender);
            myMessage.Subject = "Contact message from meetfreelunch.uk";
            myMessage.Text = subject;
            myMessage.Html = subject;

            var credentials = new NetworkCredential(
                       System.Configuration.ConfigurationManager.AppSettings["mailAccount"],
                       System.Configuration.ConfigurationManager.AppSettings["mailPassword"]
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
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