using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class MissingValuableViewModel
    {
        public string Username { get; set; }

        [DisplayName("Police Station")]
        [Required]
        public string PoliceStationName { get; set; }

        [DisplayName("Category")]
        [Required]
        public string ValuableType { get; set; }

        [DisplayName("Cost")]
        [Required]
        public string ValuableCost { get; set; }

        [DisplayName("Suspect")]
        [Required]
        public string Suspect { get; set; }

        [DisplayName("Photo")]
        public string ReciptImage { get; set; }
        public HttpPostedFileBase ValuableImageFile { get; set; }

        [DisplayName("Details")]
        [Required]
        [StringLength(200, ErrorMessage = "not more than 200 words")]
        public string Details { get; set; }
        public string CurrentDateTime { get; set; }
        public string Status { get; set; }
    }
}