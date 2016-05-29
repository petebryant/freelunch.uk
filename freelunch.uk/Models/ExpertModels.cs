using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class Expert
    {
        public string ExportId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public virtual ApplicationUser User {get; set;}
        public virtual ICollection<Specialisation> Specialisations { get; set; }
        public virtual ICollection<Link> Links { get; set; }
    }

    public class Specialisation
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public virtual Expert Expert { get; set; }
    }

    public class Link
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string URL { get; set; }
        public LinkType LinkType { get; set; }

        public virtual Expert Expert { get; set; }
    }

    public enum LinkType
    { 
        GitHub,
        Twitter,
        Blog,
        Website,
        Facebook,
        LinkedIn
    }
}