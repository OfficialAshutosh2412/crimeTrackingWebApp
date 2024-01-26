using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class NewsViewModel
    {
        [Required(ErrorMessage ="required")]
        [StringLength(50,ErrorMessage ="not more than 50 words")]
        public string Title { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(250, ErrorMessage = "not more than 250 words")]
        public string Detail { get; set; }
        public string CurrentDateTime { get; set; }
    }
}