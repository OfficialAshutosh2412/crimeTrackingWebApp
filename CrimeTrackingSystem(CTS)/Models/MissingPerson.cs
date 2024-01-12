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
    
    public partial class MissingPerson
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [DisplayName("Police Station")]
        [Required]
        public string PoliceStationName { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Person { get; set; }

        [DisplayName("Phone no.")]
        [Required]
        public decimal PersonPhone { get; set; }

        [DisplayName("Last Location")]
        [Required]
        public string LastLocation { get; set; }

        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid e-mail id")]
        public string PersonEmail { get; set; }

        [DisplayName("Reward")]
        [Required]
        public string Ransom { get; set; }

        [DisplayName("Details")]
        [Required]
        [StringLength(200,ErrorMessage ="not more than 200 words")]
        public string Details { get; set; }

        [DisplayName("Person Image")]
        public string PersonImage { get; set; }
        public HttpPostedFileBase MissingImageFile { get; set; }
        public string CurrentDateTime { get; set; }
        public string Status { get; set; }
    }
}
