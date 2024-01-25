using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class FaqViewModel
    {
        [Required(ErrorMessage = "required")]
        public string Questions { get; set; }

        [Required(ErrorMessage = "required")]
        public string Answer { get; set; }
    }
}