using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;
using System.IO;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class UserController : Controller
    {
        CTSEntities _context = new CTSEntities();
        // GET: User
        public ActionResult Index()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Index", "user");
            }
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
        //GET: Crime Complain
        public ActionResult Crime()
        {
            List<SelectListItem> options = _context.PoliceStations.Select(
                x => new SelectListItem {
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
                        crimeFormData.Proofs = "~/ProofData/" + actualFilename;

                        //mapping server image folder
                        actualFilename = Path.Combine(Server.MapPath("~/ProofData/"), actualFilename);

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
              List<SelectListItem> options = _context.PoliceStations
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.PoliceStationName })
                    .ToList();

                ViewBag.OptionList = options;
            return View();
        }
    }
}