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
            DummyLocation = new Location();
        }
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public Location DummyLocation { get; set; }

        public virtual ICollection<Specialism> Specialisms { get; set; }

        public Specialism DummySpecialism
        {
            get { return new Specialism(); }
        }

        public virtual ICollection<Link> Links { get; set; }

        public Link DummyLink
        {
            get { return new Link(); }
        }
    }
}