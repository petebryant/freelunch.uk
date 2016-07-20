using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class LunchesViewModel
    {
        public LunchesViewModel()
        {
            Lunches = new List<Lunch>();
            DummyLunch = new Lunch();
            DummyTopic = new Topic();
        }

        public virtual ICollection<Lunch> Lunches { get; set; }

        public string Search { get; set; }
        public string UserId { get; set; }

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

        //This is only used to get the Display text for it's properties
        public Lunch DummyLunch { get; private set; }
        public Topic DummyTopic { get; private set; }
    }
}