using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class EmailReplyModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage ="required")]
        public string OriginalEmail { get; set; }

        [DisplayName("Reply content")]
        [Required(ErrorMessage ="required")]
        [StringLength(200,ErrorMessage ="not more than 200 words")]
        public string ReplyContent { get; set; }
    }
}