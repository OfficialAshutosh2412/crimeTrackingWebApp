using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class HomeController : Controller
    {
        CTSEntities _context = new CTSEntities();
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

        //GET: Login
        public ActionResult Login()
        {
            return View();
        }
    }
}