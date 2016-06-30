using System;
using System.Collections.Generic;
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
    }
}
