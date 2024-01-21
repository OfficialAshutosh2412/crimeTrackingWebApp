using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;
using System.IO;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class AdminController : Controller
    {
        //GET: Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session["adminmail"] = null;
            return RedirectToAction("Login", "Home");
        }
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
            RecordViewModel data = new RecordViewModel
            {
                CrimeView = crimData,
                GeneralView = generalData,
                PersonView = personData,
                ValueView = valueData
            };

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

        //GET: Police Station
        public ActionResult PoliceStationEntry()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var prefectchData = _context.PoliceStations.ToList();
            return View(prefectchData);
        }
        //GET: Police station insertion
        public ActionResult PoliceStationEntryInsertion()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //POST: Police Station
        [HttpPost]
        public ActionResult PoliceStationEntryInsertion(PoliceStation PoliceformData)
        {
            //if form fields are validated
            if (ModelState.IsValid)
            {
                //getting only name of file
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(PoliceformData.ImageFile.FileName);

                //getting file extension
                string fileExtension = Path.GetExtension(PoliceformData.ImageFile.FileName);

                //getting posted image file
                HttpPostedFileBase postedFile = PoliceformData.ImageFile;

                //getting length of posted file
                int lengthOfFile = postedFile.ContentLength;

                //validating the file
                if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".JPEG" || fileExtension.ToLower() == ".png")
                {
                    //check if length is not more than 1MB
                    if (lengthOfFile <= 1000000)
                    {
                        //setting actuall file with extension into filename without extension
                        string actualFilename = filenameWithoutExtension + fileExtension;

                        //passing path of image with folder into database
                        PoliceformData.PoliceStationImage = "~/Uploads/PoliceStationImages/" + actualFilename;

                        //mapping server image folder
                        actualFilename = Path.Combine(Server.MapPath("~/Uploads/PoliceStationImages/"), actualFilename);

                        //saving the file into folder
                        PoliceformData.ImageFile.SaveAs(actualFilename);

                        //adding data in db
                        _context.PoliceStations.Add(PoliceformData);
                        _context.SaveChanges();
                        // Clearing ModelState to avoid displaying previous validation errors
                        ModelState.Clear();
                        TempData["result"] = "true";
                        return RedirectToAction("PoliceStationEntryInsertion","Admin");
                    }
                    else
                    {
                        TempData["SizeError"] = "<script>alert('Error : file is larger than 1MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionError"] = "<script>alert('Error : file not supported')</script>";
                }
            }
            return View();
        }
    }
}