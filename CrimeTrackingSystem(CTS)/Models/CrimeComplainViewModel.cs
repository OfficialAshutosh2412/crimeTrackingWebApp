using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class CrimeComplainViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [DisplayName("Police Station")]
        [Required(ErrorMessage = "required")]
        public string PoliceStationName { get; set; }

        [DisplayName("Category of crime")]
        [Required(ErrorMessage = "required")]
        public string CrimeType { get; set; }

        [DisplayName("Involved Person")]
        [Required(ErrorMessage = "required")]
        public string InvolvedPersons { get; set; }

        [DisplayName("Upload proofs")]
        public string Proofs { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }

        [DisplayName("Crime police Station")]
        [Required(ErrorMessage = "required")]
        public string CrimeStation { get; set; }
        public string CurrentDateTime { get; set; }
        public string Status { get; set; }
    }
}