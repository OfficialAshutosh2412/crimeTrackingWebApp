using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class CriminalImageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="required")]
        public string Name { get; set; }

        [DisplayName("Affected Organisation")]
        [Required(ErrorMessage ="required")]
        public string AffectedOrganisation { get; set; }

        [Required(ErrorMessage = "required")]
        public string Reward { get; set; }

        [DisplayName("Criminal Image")]
        public string CriminalImage { get; set; }

        [Required(ErrorMessage ="required")]
        public string Details { get; set; }
        public string CurrentDateTime { get; set; }
        public HttpPostedFileBase ImageFiles { get; set; }
    }
}