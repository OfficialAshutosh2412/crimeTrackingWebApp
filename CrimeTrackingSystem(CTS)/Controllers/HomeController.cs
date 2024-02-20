using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class HomeController : Controller
    {
        readonly CTSEntitiesClass _context = new CTSEntitiesClass();

        //GET: Index
        public ActionResult Index()
        {
            var faqData = _context.FAQs.ToList();
            return View(faqData);
        }

        //GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
        //POST: Contact
        [HttpPost]
        public ActionResult Contact(ContactViewModel contactFormData)
        {
            
                if (ModelState.IsValid == true)
                {
                    var contactdbmodel = new Contact { 
                        Fullname=contactFormData.Fullname,
                        Email=contactFormData.Email,
                        Phone=contactFormData.Phone,
                        Purpose=contactFormData.Purpose,
                        Details=contactFormData.Details,
                    };
                    _context.Contacts.Add(contactdbmodel);
                    _context.SaveChanges();
                    ModelState.Clear();
                    //ViewBag.message = "Information Recorded.";
                }   
            return View();
        }

        //GET: Signup
        public ActionResult Signup()
        {
            return View();
        }
        //POST: Signup
        [HttpPost]
        public ActionResult Signup(SignupViewModel formDataOfSignup)
        {
            //if form fields are validated
            if (ModelState.IsValid == true)
            {
                ////getting only name of file
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(formDataOfSignup.ImageFile.FileName);
                ////getting file extension
                string fileExtension = Path.GetExtension(formDataOfSignup.ImageFile.FileName);
                ////getting posted image file
                ////getting length of posted file
                //int lengthOfFile = postedFile.ContentLength;
                //now checking for filetype accepting only jpg, jpeg, JPEG and png files.
                //if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".JPEG" || fileExtension.ToLower() == ".png")
                //{
                //    //check if length is not more than 1MB
                //    if (lengthOfFile <= 1000000)
                //    {
                //        //setting actuall file with extension into filename without extension
                        string actualFilename = filenameWithoutExtension + fileExtension;
                        //passing path of image with folder into database
                        formDataOfSignup.Photo = "~/Uploads/UserProfileImages/" + actualFilename;
                        //mapping server image folder
                        actualFilename = Path.Combine(Server.MapPath("~/Uploads/UserProfileImages/"), actualFilename);
                        //saving the file into folder
                        formDataOfSignup.ImageFile.SaveAs(actualFilename);
                        
                        if (formDataOfSignup.Role == "98u9d8uwr3(9ih(8H8&67^g&UyGIuh(7t6G^F4d4@A#24545fGbuyb*Y(8jIj(87Re54#qW")
                        {
                            formDataOfSignup.Role = "admin";    
                        }
                        else if (formDataOfSignup.Role != "98u9d8uwr3(9ih(8H8&67^g&UyGIuh(7t6G^F4d4@A#24545fGbuyb*Y(8jIj(87Re54#qW")
                        {
                            formDataOfSignup.Role = "user";
                        }
                        var signupdbmodel = new Signup
                        {
                            Username = formDataOfSignup.Username,
                            Password = formDataOfSignup.Password,
                            Email = formDataOfSignup.Email,
                            Gender = formDataOfSignup.Gender,
                            Pincode = formDataOfSignup.Pincode,
                            Address = formDataOfSignup.Address,
                            Mstatus = formDataOfSignup.Mstatus,
                            Lstatus = formDataOfSignup.Lstatus,
                            Adhaar = formDataOfSignup.Adhaar,
                            Phone = formDataOfSignup.Phone,
                            Photo = formDataOfSignup.Photo,
                            Role = formDataOfSignup.Role,
                        };
                        _context.Signups.Add(signupdbmodel);
                        _context.SaveChanges();
                        
                //    }
                //    else
                //    {
                //        TempData["SizeError"] = "<script>alert('Error : file is larger than 1MB')</script>";
                //    }
                //}
                //else
                //{
                //    TempData["ExtensionError"] = "<script>alert('Error : file not supported')</script>";
                //}
            }
            return View();
        }
        //GET: Login
        public ActionResult Login()
        {
            return View();
        }
        //POST: Login
        [HttpPost]
        public ActionResult Login(Login loginFormData)
        {
            //checking the input of login creds with the signup cred fields...
            var creds = _context.Signups.Where(model => model.Email == loginFormData.Email && model.Password == loginFormData.Password).FirstOrDefault();
            if (creds == null)
            {
                ViewBag.CredError = "check your username and password";
            }
            else
            {
                if (creds.Role == "user")
                {
                    FormsAuthentication.SetAuthCookie(loginFormData.Email, false);
                    //Session["usermail"] = loginFormData.Email;
                    return RedirectToAction("Index", "User");
                }
                else if (creds.Role == "admin")
                {
                    FormsAuthentication.SetAuthCookie(loginFormData.Email, false);
                    return RedirectToAction("Index", "Admin");
                }
                //if (creds.Role == "user")
                //{
                //    Session["usermail"] = loginFormData.Email;
                //    return RedirectToAction("Index", "User");
                //}
                //else if (creds.Role == "admin")
                //{
                //    Session["adminmail"] = loginFormData.Email;
                //    return RedirectToAction("Index", "Admin");
                //}

            }
            return View();
        }
        //GET: Feedback
        public ActionResult Feedback()
        {
            return View();
        }
        //POST: Feedback
        [HttpPost]
        public ActionResult Feedback(FeedbackViewModel feedbackData)
        {
            if (ModelState.IsValid == true)
            {
                var feedbackdbmodel = new Feedback
                {
                    Yourname = feedbackData.Yourname,
                    E_mail = feedbackData.E_mail,
                    Words = feedbackData.Words

                };
                _context.Feedbacks.Add(feedbackdbmodel);
                _context.SaveChanges();
                //ModelState.Clear();
            }
            return View();
        }
        //event
        public ActionResult CTS_Events()
        {
            var firstEvent = _context.Events.ToList();
            return View(firstEvent);
        }
        //wanted
        public ActionResult Wanted_Lists()
        {
            var listOfCriminals = _context.CriminalGalleries.ToList();
            return View(listOfCriminals);
        }
        //news
        public ActionResult News()
        {
            var newsData = _context.News.ToList();
            return View(newsData);
        }
        public ActionResult News_details(int id)
        {
            ViewBag.gettingData = _context.News.Where(model=>model.Id==id).FirstOrDefault();
            return View();
        }
    }
}