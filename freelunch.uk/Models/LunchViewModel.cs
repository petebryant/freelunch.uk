using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class LunchViewModel
    {
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Website { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Type of Audience")]
        public string Audience { get; set; }

        [Display(Name = "Size of Audience")]
        public string AudienceNumber { get; set; }

        [Url]
        public string Image { get; set; }
    }
}