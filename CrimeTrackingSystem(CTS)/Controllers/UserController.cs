using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["usermail"] == null)
            {
                return RedirectToAction("Index", "user");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session["usermail"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}