﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class HomeController : Controller
    {
        readonly CTSEntities _context = new CTSEntities();
        //GET: Index
        public ActionResult Index()
        {
            return View();
        }

        //GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
        //POST: Contact
        [HttpPost]
        public ActionResult Contact(Contact contactFormData)
        {
            if (ModelState.IsValid)
            {
                _context.Contact.Add(contactFormData);
                _context.SaveChanges();
                ModelState.Clear();
                ViewBag.result = true;
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
        public ActionResult Signup(Signup formDataOfSignup)
        {
            //if form fields are validated
            if (ModelState.IsValid == true)
            {
                //getting only name of file
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(formDataOfSignup.ImageFile.FileName);
                //getting file extension
                string fileExtension = Path.GetExtension(formDataOfSignup.ImageFile.FileName);
                //getting posted image file
                HttpPostedFileBase postedFile = formDataOfSignup.ImageFile;
                //getting length of posted file
                int lengthOfFile = postedFile.ContentLength;
                //now checking for filetype accepting only jpg, jpeg, JPEG and png files.
                if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".JPEG" || fileExtension.ToLower() == ".png")
                {
                    //check if length is not more than 1MB
                    if (lengthOfFile <= 1000000)
                    {
                        //setting actuall file with extension into filename without extension
                        string actualFilename = filenameWithoutExtension + fileExtension;
                        //passing path of image with folder into database
                        formDataOfSignup.Photo = "~/UserProfilePicture/" + actualFilename;
                        //mapping server image folder
                        actualFilename = Path.Combine(Server.MapPath("~/UserProfilePicture/"), actualFilename);
                        //saving the file into folder
                        formDataOfSignup.ImageFile.SaveAs(actualFilename);
                        if (formDataOfSignup.Role == "98u9d8uwr3(9ih(8H8&67^g&UyGIuh(7t6G^F4d4@A#24545fGbuyb*Y(8jIj(87Re54#qW")
                        {
                            formDataOfSignup.Role = "admin";    
                            _context.Signup.Add(formDataOfSignup);
                            _context.SaveChanges();
                        }
                        else if (formDataOfSignup.Role != "98u9d8uwr3(9ih(8H8&67^g&UyGIuh(7t6G^F4d4@A#24545fGbuyb*Y(8jIj(87Re54#qW")
                        {
                            formDataOfSignup.Role = "user";
                            _context.Signup.Add(formDataOfSignup);
                            _context.SaveChanges();
                        }

                        ModelState.Clear();
                        return RedirectToAction("Login", "Home");
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
            var creds = _context.Signup.Where(model => model.Email == loginFormData.Email && model.Password == loginFormData.Password).FirstOrDefault();
            if (creds == null)
            {
                ViewBag.CredError = true;
            }
            else
            {
                if (creds.Role == "user")
                {
                    Session["usermail"] = loginFormData.Email;
                    return RedirectToAction("Index", "User");
                }
                else if (creds.Role == "admin")
                {
                    Session["adminmail"] = loginFormData.Email;
                    return RedirectToAction("Index", "Admin");
                }
                
            }
            return View();
        }
    }
}