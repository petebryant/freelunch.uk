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
            Specialisms = new List<Specialism>();
            Locations = new List<Location>();
            DisplayLocation = new Location();
            DummyLink = new Link();
            DummySpecialism = new Specialism();
        }
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public Location DisplayLocation { get; set; }

        public virtual ICollection<Specialism> Specialisms { get; set; }

        public virtual ICollection<Link> Links { get; set; }


        //These are only used to get the Display text for their properties
        public Link DummyLink { get; private set; }
        public Specialism DummySpecialism { get; private set; }
    }
}