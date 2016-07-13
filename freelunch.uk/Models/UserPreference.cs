using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace freelunch.uk.Models
{
    public class UserPreference
    {
        public UserPreference()
        {
            ReceiveMFLMailing = true;
            ReceivePartnerMailing = true;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

         [Display(Name = "meetfreelunck.uk")]
        public bool ReceiveMFLMailing { get; set; }

        [Display(Name = "Our partners")]
        public bool ReceivePartnerMailing { get; set; }
    }
}