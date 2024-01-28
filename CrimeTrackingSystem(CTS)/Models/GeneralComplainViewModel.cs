using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class GeneralComplainViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [DisplayName("Police Station")]
        [Required(ErrorMessage = "required")]
        public string PoliceStationName { get; set; }

        [Required(ErrorMessage = "required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(200, ErrorMessage = "details not more than 200 words")]
        public string Details { get; set; }

        [DisplayName("Involved Person")]
        [Required(ErrorMessage = "required")]
        public string InvolvedPersons { get; set; }
        public string CurrentDateTime { get; set; }
        public string Status { get; set; }
    }
}