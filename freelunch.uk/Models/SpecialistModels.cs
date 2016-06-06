using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class Specialist
    {
        public Specialist()
        {
            Links = new List<Link>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Specialisation> Specialisations { get; set; }
        public virtual ICollection<Link> Links { get; set; }
    }

    public class Specialisation
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public virtual Specialist Specialist { get; set; }
    }

    public class Link
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Text { get; set; }

        [Required]
        [Url]
        [Display(Name = "Link")]
        public string URL { get; set; }

        [Required]
        [Display(Name = "Type")]
        public LinkType LinkType { get; set; }

        public virtual Specialist Specialist { get; set; }
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