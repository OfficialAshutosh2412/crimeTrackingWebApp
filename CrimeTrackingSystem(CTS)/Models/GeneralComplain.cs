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
    
    public partial class GeneralComplain
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [DisplayName("Police Station")]
        [Required]
        public string PoliceStationName { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [StringLength(200, ErrorMessage ="details not more than 200 words")]
        public string Details { get; set; }

        [DisplayName("Involved Person")]
        [Required]
        public string InvolvedPersons { get; set; }
        public string CurrentDateTime { get; set; }
        public string Status { get; set; }
    }
}
