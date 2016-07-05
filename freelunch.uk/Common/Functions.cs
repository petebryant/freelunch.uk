using freelunch.uk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using freelunch.uk.Models;
using SendGrid;
using System.Net;

namespace freelunch.uk.Common
{
    public class Functions
    {
        public static bool IsSpecialist(string userId)
        {
            if (userId == null) return false;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var specialist = context.Specialists.FirstOrDefault(x => x.UserId == userId);

                return (specialist != null);
            }
        }

        public static async Task SendGridAsync(string to, string message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(to);
            myMessage.From = new System.Net.Mail.MailAddress(
                                "info@meetfreelunch.uk", "meetfreelunch.uk");
            myMessage.Subject = "Contact message from meetfreelunch.uk";
            myMessage.Text = message;
            myMessage.Html = message;

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
                await Task.FromResult(0);
            }
        }
    }
}