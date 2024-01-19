using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrimeTrackingSystem_CTS_.Models
{
    public class RecordViewModel
    {
        public List<CrimeComplain> CrimeView { get; set; }
        public List<GeneralComplain> GeneralView { get; set; }
        public List<MissingPerson> PersonView { get; set; }
        public List<MissingValuable> ValueView { get; set; }
    }
}