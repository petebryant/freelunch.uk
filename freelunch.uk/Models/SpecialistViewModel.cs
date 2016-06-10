using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class SpecialistViewModel
    {
        public SpecialistViewModel()
        {
            Links = new List<Link>();
        }
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public virtual ICollection<Link> Links { get; set; }

        public Link DummyLink
        {
            get { return new Link(); }
        }
    }
}