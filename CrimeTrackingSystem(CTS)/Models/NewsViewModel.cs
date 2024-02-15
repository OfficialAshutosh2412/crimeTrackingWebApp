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
        public string Title { get; set; }

        [Required(ErrorMessage = "required")]
        public string Detail { get; set; }
        public string CurrentDateTime { get; set; }
    }
}