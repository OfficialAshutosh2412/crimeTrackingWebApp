using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class Login
    {
        [DisplayName("Enter your E-mail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid e-mail id")]
        public string Email { get; set; }

        [DisplayName("Enter your password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}