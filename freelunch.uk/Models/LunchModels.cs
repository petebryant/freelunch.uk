using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class Lunch
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Type of Audience")]
        public string Audience { get; set; }

        [Display(Name = "Size")]
        public string AudienceNumber { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }

    public class Topic
    {
        public Topic()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Topics")]
        public string Name { get; set; }

        public virtual Lunch Lunch { get; set; }
    }
}