using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class AdminController : Controller
    {
        CTSEntitiesClass _context = new CTSEntitiesClass();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //GET: Records
        public ActionResult RecordRoom()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var crimData = GetCrimeList();
            var generalData = GetGeneralList();
            var personData = GetPersonList();
            var valueData = GetValueList();
            RecordViewModel data = new RecordViewModel();
            data.CrimeView = crimData;
            data.GeneralView = generalData;
            data.PersonView = personData;
            data.ValueView = valueData;

            return View(data);
        }
        //Getting crimecomplains as list.
        public List<CrimeComplain> GetCrimeList()
        {
            return _context.CrimeComplains.ToList();
        }
        //Getting generalcomplains as list.
        public List<GeneralComplain> GetGeneralList()
        {
            return _context.GeneralComplains.ToList();
        }
        //Getting missingperson as list.
        public List<MissingPerson> GetPersonList()
        {
            return _context.MissingPersons.ToList();
        }
        //Getting mssingvalues as list.
        public List<MissingValuable> GetValueList()
        {
            return _context.MissingValuables.ToList();
        }
    }
}