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

    public partial class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "invalid e-mail id")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(\d{10}|\d{12})$", ErrorMessage = "invalid phone number")]
        public decimal Phone { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "not more than 50 words")]
        public string Purpose { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "not more than 250 words")]
        public string Details { get; set; }
    }
}
