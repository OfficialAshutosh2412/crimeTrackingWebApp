using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "required")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid e-mail id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^(\d{10}|\d{12})$", ErrorMessage = "invalid phone number")]
        [StringLength(12, ErrorMessage = "should be 10 or 12 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(50, ErrorMessage = "not more than 50 words")]
        public string Purpose { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(250, ErrorMessage = "not more than 250 words")]
        public string Details { get; set; }
    }
}