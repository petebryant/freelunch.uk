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
        }

        public virtual ICollection<Specialist> Specialists { get; set; }


        public Specialist DummySpecialist { get { return new Specialist(); } }
        public Location DummyLocation { get { return new Location(); } }

        [Required]
        [Display(Name = "Name:")]
        public string Sender { get; set; }
        [Required, EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Message:")]
        public string Message { get; set; }
        public string URL { get; set; }
        public string Search { get; set; }
        public string UserId { get; set; }
    }
}
