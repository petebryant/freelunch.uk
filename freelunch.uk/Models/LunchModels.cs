﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class Lunch
    {
        public Lunch()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ContactName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Audience { get; set; }
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