using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class ExpertViewModel
    {
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Display Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}