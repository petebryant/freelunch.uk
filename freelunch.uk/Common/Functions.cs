using freelunch.uk.Models;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace freelunch.uk.Common
{
    public class Functions
    {
        public static bool IsSpecialist(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var specialist = context.Specialists.FirstOrDefault(x => x.UserId == userId);

                return (specialist != null);
            }
        }

        public static bool HasLunch(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var specialist = context.Lunches.FirstOrDefault(x => x.UserId == userId);

                return (specialist != null);
            }
        }

        public static bool HasPassword(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var user = manager.FindById(userId);
                if (user != null)
                {
                    return user.PasswordHash != null;
                }
                return false;
            }
        }

        public static bool HasPhoneNumber(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var phoneNumber = manager.GetPhoneNumber(userId);

                return (phoneNumber != null);
            }
        }

        public static bool HasTwoFactorEnabled(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                return manager.GetTwoFactorEnabled(userId);
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

        public static UserPreference UserPreferences(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new UserPreference();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                UserPreference pref = context.Preferences.SingleOrDefault(p => p.UserId == userId);

                if (pref == null) pref = new UserPreference();
                return pref;
            }
        }
    }
}