﻿using freelunch.uk.Models;
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
        public static bool IsSpecialist(string userId)
        {
            if (userId == null) return false;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var specialist = context.Specialists.FirstOrDefault(x => x.UserId == userId);

                return (specialist != null);
            }
        }
    }
}