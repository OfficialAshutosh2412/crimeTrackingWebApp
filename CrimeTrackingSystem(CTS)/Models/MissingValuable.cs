//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrimeTrackingSystem_CTS_.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class MissingValuable
    {
        public int Id { get; set; }
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
