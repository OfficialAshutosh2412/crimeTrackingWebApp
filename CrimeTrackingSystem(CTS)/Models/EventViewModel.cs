using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [DisplayName("Event Title")]
        [Required(ErrorMessage ="required")]
        [StringLength(50,ErrorMessage ="not more than 50 words")]
        public string EventTitle { get; set; }

        [DisplayName("Event Date")]
        [Required(ErrorMessage = "required")]
        public string EventDateTime { get; set; }
        public string CurrentDateTime { get; set; }

        [DisplayName("Event Images")]
        public string EventImage { get; set; }

        public List<HttpPostedFileBase> ImageFiles { get; set; }
    }
}