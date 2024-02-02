using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class SignupViewModel
    {
        [DisplayName("Your fullname")]
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [DisplayName("Create password")]
        [Required]
        [RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8,}$", ErrorMessage = "password must: atleast 8-char long, 2 - uppercase, 3 - lowercase, 1 - special char, 2 - digits")]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password doesn't match")]
        public string ConfirmPassword { get; set; }

        [DisplayName("E-mail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid e-mail id")]
        public string Email { get; set; }

        [DisplayName("Gender")]
        [Required]
        public string Gender { get; set; }

        [Required]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "invalid pincode")]
        public string Pincode { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DisplayName("Marital Status")]
        public string Mstatus { get; set; }

        [Required]
        [DisplayName("Living Status")]
        public string Lstatus { get; set; }

        [Required]
        [DisplayName("Adhaar No.")]
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "invalid adhaar number")]
        public string Adhaar { get; set; }

        [Required]
        [RegularExpression(@"^(\d{10}|\d{12})$", ErrorMessage = "invalid phone number")]
        public string Phone { get; set; }
        public string Photo { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        public string Role { get; set; }
    }
}