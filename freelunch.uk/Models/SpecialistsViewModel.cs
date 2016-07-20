using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace freelunch.uk.Models
{
    public class SpecialistsViewModel
    {
        public SpecialistsViewModel()
        {
            Specialists = new List<Specialist>();
            DummySpecialist = new Specialist();
            DummyLocation = new Location();
        }

        public virtual ICollection<Specialist> Specialists { get; set; }


        //These are only used to get the Display text for their properties
        public Specialist DummySpecialist { get; private set; }
        public Location DummyLocation { get; private set; }

        [Required]
        [Display(Name = "Name")]
        public string Sender { get; set; }
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
        public string URL { get; set; }
        public string Search { get; set; }
        public string UserId { get; set; }
    }
}
