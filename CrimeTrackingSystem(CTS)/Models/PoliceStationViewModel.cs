using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class PoliceStationViewModel
    {
        [DisplayName("Chowki Incharge Name")]
        [Required(ErrorMessage = "required")]
        public string ChowkiIncharge { get; set; }

        [DisplayName("Police Station Name")]
        [Required(ErrorMessage = "required")]
        public string PoliceStationName { get; set; }

        [DisplayName("CUG Number 1")]
        [Required(ErrorMessage = "required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "only digit allowed")]
        [StringLength(10, ErrorMessage = "only 10-digits allowed")]
        public string CUGNumberOne { get; set; }

        [DisplayName("CUG Number 2")]
        [Required(ErrorMessage = "required")]
        [StringLength(10, ErrorMessage = "only 10-digits allowed")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "only digit allowed")]
        public string CUGNumberSecond { get; set; }

        [DisplayName("Agent Name")]
        [Required(ErrorMessage = "required")]
        public string Agent { get; set; }

        [DisplayName("Agent Phone no.")]
        [Required(ErrorMessage = "required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "only digit allowed")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string AgentPhone { get; set; }

        [DisplayName("Police Station Image")]
        public string PoliceStationImage { get; set; }

        [Required(ErrorMessage = "required")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}