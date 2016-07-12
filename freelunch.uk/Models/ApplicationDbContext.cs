using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Specialist> Specialists { get; set; }

        public DbSet<Lunch> Lunches { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<Specialism> Specialisms { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<UserPreference> Preferences { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}