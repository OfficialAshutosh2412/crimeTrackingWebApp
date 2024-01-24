using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class MissingPersonViewModel
    {
        public string Username { get; set; }

        [DisplayName("Police Station")]
        [Required(ErrorMessage = "required")]
        public string PoliceStationName { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "required")]
        public string Person { get; set; }

        [DisplayName("Phone no.")]
        [Required(ErrorMessage = "required")]
        [StringLength(12, ErrorMessage = "should be in 10 or 12 digits")]
        public string PersonPhone { get; set; }

        [DisplayName("Last Location")]
        [Required(ErrorMessage = "required")]
        public string LastLocation { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid e-mail id")]
        public string PersonEmail { get; set; }

        [DisplayName("Reward")]
        [Required(ErrorMessage = "required")]
        public string Ransom { get; set; }

        [DisplayName("Details")]
        [Required(ErrorMessage = "required")]
        [StringLength(200, ErrorMessage = "not more than 200 words")]
        public string Details { get; set; }

        [DisplayName("Person Image")]
        public string PersonImage { get; set; }
        public HttpPostedFileBase MissingImageFile { get; set; }
        public string CurrentDateTime { get; set; }
        public string Status { get; set; }
    }
}