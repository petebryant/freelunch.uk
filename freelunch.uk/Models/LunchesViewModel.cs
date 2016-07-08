using System;
using System.Collections.Generic;
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
        }

        public virtual ICollection<Lunch> Lunches { get; set; }

        public string Search { get; set; }

        //This is only used to get the Display text for it's properties
        public Lunch DummyLunch { get; private set; }
    }
}