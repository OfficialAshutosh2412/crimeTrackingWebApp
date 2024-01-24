using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class FeedbackViewModel
    {
        [DisplayName("Your name")]
        [Required(ErrorMessage = "required")]
        public string Yourname { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid e-mail id")]
        public string E_mail { get; set; }

        [Required(ErrorMessage = "required")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "not more than 200 words")]
        public string Words { get; set; }
    }
}