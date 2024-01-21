using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;
using System.IO;
using System.Data.Entity;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class UserController : Controller
    {
        readonly CTSEntitiesClass _context = new CTSEntitiesClass();
        // GET: User
        public ActionResult Index()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var sessiondata = (string)Session["usermail"];
            //crime
            var solvedcrimelist = _context.CrimeComplains.Where(model => model.Username == sessiondata && model.Status == "Solved").ToList();
            var pendcrimelist = _context.CrimeComplains.Where(model => model.Username == sessiondata && model.Status == "Pending").ToList();
            var solvedcount = solvedcrimelist.Count();
            var pendcount = pendcrimelist.Count();
            ViewBag.solved = solvedcount;
            ViewBag.pending = pendcount;
            //general
            var solvedgenerallist = _context.GeneralComplains.Where(model => model.Username == sessiondata && model.Status == "Solved").ToList();
            var pendgenerallist = _context.GeneralComplains.Where(model => model.Username == sessiondata && model.Status == "Pending").ToList();
            var solvedgeneralcount = solvedgenerallist.Count();
            var pendgeneralcount = pendgenerallist.Count();
            ViewBag.solvedgeneral = solvedgeneralcount;
            ViewBag.pendinggeneral = pendgeneralcount;
            //person
            var solvedpersonlist = _context.MissingPersons.Where(model => model.Username == sessiondata && model.Status == "Solved").ToList();
            var pendpersonlist = _context.MissingPersons.Where(model => model.Username == sessiondata && model.Status == "Pending").ToList();
            var solvedpersoncount = solvedpersonlist.Count();
            var pendpersoncount = pendpersonlist.Count();
            ViewBag.solvedperson = solvedpersoncount;
            ViewBag.pendingperson = pendpersoncount;
            //valuable
            var solvedvaluelist = _context.MissingValuables.Where(model => model.Username == sessiondata && model.Status == "Solved").ToList();
            var pendvaluelist = _context.MissingValuables.Where(model => model.Username == sessiondata && model.Status == "Pending").ToList();
            var solvedvaluecount = solvedvaluelist.Count();
            var pendvaluecount = pendvaluelist.Count();
            ViewBag.solvedvalue = solvedvaluecount;
            ViewBag.pendingvalue = pendvaluecount;
            return View();
        }
        //GET: Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session["usermail"] = null;
            return RedirectToAction("Login", "Home");
        }
        //GET:Profile
        public ActionResult MyProfile()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var session = (string)Session["usermail"];
            var profiledata = _context.Signups.FirstOrDefault(model => model.Email == session);

            if (profiledata == null)
            {
                // Handle the case where no profile data is found for the user
                return RedirectToAction("Index", "User");
            }
            ViewBag.profile = profiledata;
            return View();
        }
        //GET:EditProfile
        public ActionResult EditProfile()
        {
            var session = (string)Session["usermail"];
            var profiledata = _context.Signups.Where(model => model.Email == session).FirstOrDefault();
            Session["image"] = profiledata.Photo.ToString();
            return View(profiledata);
        }
        //POST:EditProfile
        [HttpPost]
        public ActionResult EditProfile(Signup editFormData)
        {
            if (ModelState.IsValid == true)
            {
                if (editFormData.ImageFile != null)
                {
                    HttpPostedFileBase postedFile = editFormData.ImageFile;
                    string filenameWithoutExtension = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    int lengthOfFile = postedFile.ContentLength;
                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".JPEG" || fileExtension.ToLower() == ".png")
                    {
                        if (lengthOfFile <= 1000000)
                        {
                            string actualFilename = filenameWithoutExtension + fileExtension;
                            editFormData.Photo = "~/Uploads/UserProfileImages/" + actualFilename;
                            actualFilename = Path.Combine(Server.MapPath("~/Uploads/UserProfileImages/"), actualFilename);
                            postedFile.SaveAs(actualFilename);
                            _context.Entry(editFormData).State = EntityState.Modified;
                            int a = _context.SaveChanges();
                            if (a > 0)
                            {
                                TempData["update"] = "true";
                            }
                            return RedirectToAction("MyProfile", "User");
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
                else
                {
                    editFormData.Photo = Session["image"].ToString();
                    _context.Entry(editFormData).State = EntityState.Modified;
                    int a = _context.SaveChanges();
                    if (a > 0)
                    {
                        TempData["update"] = "true";
                    }
                    return RedirectToAction("MyProfile", "User");
                }
            }
            return View();
        }

        //GET: Crime Complain
        public ActionResult Crime()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<SelectListItem> options = _context.PoliceStations.Select(
                x => new SelectListItem
                {
                    Value = x.PoliceStationName.ToString(),
                    Text = x.PoliceStationName
                }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //POST: Crime Complain
        [HttpPost]
        public ActionResult Crime(CrimeComplain crimeFormData)
        {
            //if form fields are validated
            if (ModelState.IsValid)
            {
                //getting only name of file
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(crimeFormData.UploadImage.FileName);

                //getting file extension
                string fileExtension = Path.GetExtension(crimeFormData.UploadImage.FileName);

                //getting posted image file
                HttpPostedFileBase postedFile = crimeFormData.UploadImage;

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
                        crimeFormData.Proofs = "~/Uploads/CrimeComplainProofs/" + actualFilename;

                        //mapping server image folder
                        actualFilename = Path.Combine(Server.MapPath("~/Uploads/CrimeComplainProofs/"), actualFilename);

                        //saving the file into folder
                        crimeFormData.UploadImage.SaveAs(actualFilename);

                        //adding data in db
                        crimeFormData.Username = (string)Session["usermail"];
                        crimeFormData.CurrentDateTime = DateTime.Now.ToString();
                        crimeFormData.Status = "Pending";
                        _context.CrimeComplains.Add(crimeFormData);
                        _context.SaveChanges();

                        // Clearing ModelState to avoid displaying previous validation errors
                        ModelState.Clear();
                        TempData["result"] = "true";
                        return RedirectToAction("Crime", "User");
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
            // If ModelState is not valid
            List<SelectListItem> options = _context.PoliceStations.Select(
               x => new SelectListItem
               {
                   Value = x.PoliceStationName.ToString(),
                   Text = x.PoliceStationName
               }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //GET:General Complain
        public ActionResult General()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<SelectListItem> options = _context.PoliceStations.Select(
                x => new SelectListItem
                {
                    Value = x.PoliceStationName.ToString(),
                    Text = x.PoliceStationName
                }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //POST:General
        [HttpPost]
        public ActionResult General(GeneralComplain generalFormData)
        {
            if (ModelState.IsValid)
            {
                //adding data in db
                generalFormData.Username = (string)Session["usermail"];
                generalFormData.CurrentDateTime = DateTime.Now.ToString();
                generalFormData.Status = "Pending";
                _context.GeneralComplains.Add(generalFormData);
                _context.SaveChanges();
                // Clearing ModelState to avoid displaying previous validation errors
                ModelState.Clear();
                TempData["result"] = "true";
                return RedirectToAction("General", "User");
            }
            List<SelectListItem> options = _context.PoliceStations.Select(
                x => new SelectListItem
                {
                    Value = x.PoliceStationName.ToString(),
                    Text = x.PoliceStationName
                }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //GET: Person
        public ActionResult Person()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<SelectListItem> options = _context.PoliceStations.Select(
                x => new SelectListItem
                {
                    Value = x.PoliceStationName.ToString(),
                    Text = x.PoliceStationName
                }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //POST: Person
        [HttpPost]
        public ActionResult Person(MissingPerson personFormData)
        {
            //if form fields are validated
            if (ModelState.IsValid)
            {
                //getting only name of file
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(personFormData.MissingImageFile.FileName);

                //getting file extension
                string fileExtension = Path.GetExtension(personFormData.MissingImageFile.FileName);

                //getting posted image file
                HttpPostedFileBase postedFile = personFormData.MissingImageFile;

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
                        personFormData.PersonImage = "~/Uploads/MissingPersonImages/" + actualFilename;

                        //mapping server image folder
                        actualFilename = Path.Combine(Server.MapPath("~/Uploads/MissingPersonImages/"), actualFilename);

                        //saving the file into folder
                        personFormData.MissingImageFile.SaveAs(actualFilename);

                        //adding data in db
                        personFormData.Username = (string)Session["usermail"];
                        personFormData.CurrentDateTime = DateTime.Now.ToString();
                        personFormData.Status = "Pending";
                        _context.MissingPersons.Add(personFormData);
                        _context.SaveChanges();

                        // Clearing ModelState to avoid displaying previous validation errors
                        ModelState.Clear();
                        TempData["result"] = "true";
                        return RedirectToAction("Person", "User");
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
            // If ModelState is not valid
            List<SelectListItem> options = _context.PoliceStations.Select(
                 x => new SelectListItem
                 {
                     Value = x.PoliceStationName.ToString(),
                     Text = x.PoliceStationName
                 }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //GET:Valuable
        public ActionResult Valuable()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<SelectListItem> options = _context.PoliceStations.Select(
                 x => new SelectListItem
                 {
                     Value = x.PoliceStationName.ToString(),
                     Text = x.PoliceStationName
                 }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //POST: Valuable
        [HttpPost]
        public ActionResult Valuable(MissingValuable valueFormData)
        {
            //if form fields are validated
            if (ModelState.IsValid)
            {
                //getting only name of file
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(valueFormData.ValuableImageFile.FileName);

                //getting file extension
                string fileExtension = Path.GetExtension(valueFormData.ValuableImageFile.FileName);

                //getting posted image file
                HttpPostedFileBase postedFile = valueFormData.ValuableImageFile;

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
                        valueFormData.ReciptImage = "~/Uploads/MissingValuableImages/" + actualFilename;

                        //mapping server image folder
                        actualFilename = Path.Combine(Server.MapPath("~/Uploads/MissingValuableImages/"), actualFilename);

                        //saving the file into folder
                        valueFormData.ValuableImageFile.SaveAs(actualFilename);

                        //adding data in db
                        valueFormData.Username = (string)Session["usermail"];
                        valueFormData.CurrentDateTime = DateTime.Now.ToString();
                        valueFormData.Status = "Pending";
                        _context.MissingValuables.Add(valueFormData);
                        _context.SaveChanges();

                        // Clearing ModelState to avoid displaying previous validation errors
                        ModelState.Clear();
                        TempData["result"] = "true";
                        return RedirectToAction("Valuable", "User");
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
            List<SelectListItem> options = _context.PoliceStations.Select(
                x => new SelectListItem
                {
                    Value = x.PoliceStationName.ToString(),
                    Text = x.PoliceStationName
                }).ToList();

            ViewBag.OptionList = options;
            return View();
        }
        //show crime complain data
        public ActionResult CrimeComplainData()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var userId = (string)Session["usermail"];
            var userData = _context.CrimeComplains.Where(model => model.Username == userId).ToList();
            return View(userData);
        }
        //show general complain data
        public ActionResult GeneralComplainData()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var userId = (string)Session["usermail"];
            var userData = _context.GeneralComplains.Where(model => model.Username == userId).ToList();
            return View(userData);
        }
        //show missing person complain data
        public ActionResult MissingPersonData()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var userId = (string)Session["usermail"];
            var userData = _context.MissingPersons.Where(model => model.Username == userId).ToList();
            return View(userData);
        }
        //show missing valuable complain data
        public ActionResult MissingValuableData()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var userId = (string)Session["usermail"];
            var userData = _context.MissingValuables.Where(model => model.Username == userId).ToList();
            return View(userData);
        }
    }
}