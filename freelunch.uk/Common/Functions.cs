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

namespace freelunch.uk.Common
{
    public class Functions
    {
        public static bool IsExpert(string userId)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var expert = context.Experts.FirstOrDefault(x => x.UserId == userId);

            return (expert != null);
        }
    }
}