using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeTrackingSystem_CTS_.Models;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data.Entity;

namespace CrimeTrackingSystem_CTS_.Controllers
{
    public class AdminController : Controller
    {
        //GET: Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
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
        public ActionResult PoliceStationEntryInsertion(PoliceStationViewModel PoliceformData)
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
                        var policedbmodel = new PoliceStation
                        {
                            ChowkiIncharge=PoliceformData.ChowkiIncharge,
                            PoliceStationName=PoliceformData.PoliceStationName,
                            CUGNumberOne=PoliceformData.CUGNumberOne,
                            CUGNumberSecond=PoliceformData.CUGNumberSecond,
                            Agent=PoliceformData.Agent,
                            AgentPhone=PoliceformData.AgentPhone,
                            PoliceStationImage=PoliceformData.PoliceStationImage,
                        };
                        _context.PoliceStations.Add(policedbmodel);
                        _context.SaveChanges();
                        // Clearing ModelState to avoid displaying previous validation errors
                        ModelState.Clear();
                        TempData["result"] = "true";
                        return RedirectToAction("PoliceStationEntryInsertion", "Admin");
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
        //GET : Contact Requests
        public ActionResult ContactRequests()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var contactData = _context.Contacts.ToList();
            return View(contactData);
        }
        //GET : Contact Request Reply
        public ActionResult ContactRequestReply()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Contact> emailList = _context.Contacts.ToList();
            SelectList emailSelectList = new SelectList(emailList, "Email", "Email");
            ViewBag.listOfEmail = emailSelectList;
            var replyModel = new EmailReplyModel();
            return View(replyModel);
        }
        //POST: Contact request reply
        [HttpPost]
        public ActionResult ContactRequestReply(EmailReplyModel replyFormData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("start sending");
                    Contact selectedEmailAddress = _context.Contacts.FirstOrDefault(x => x.Email == replyFormData.OriginalEmail);
                    if (selectedEmailAddress != null)
                    {
                        string smtpUserName;
                        string smtpPassword;

                        smtpUserName = System.Configuration.ConfigurationManager.AppSettings["myMailAddress"];
                        smtpPassword = System.Configuration.ConfigurationManager.AppSettings["usernameForAspDotNetMVC"];
                        MailMessage mail = new MailMessage();
                        SmtpClient smtp_Client = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["smtpClient"]);                                                
                        smtp_Client.Port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["smtpPort"]);
                        smtp_Client.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["enableSSL"]);
                        mail.From = new MailAddress(smtpUserName); 
                        mail.To.Add(selectedEmailAddress.Email);
                        mail.Subject = "Regarding your contact request to CTS Portal";
                        mail.Body = ("Name : " + selectedEmailAddress.Fullname.ToString() + Environment.NewLine + "Message : " + replyFormData.ReplyContent.ToString());
                        bool useDefaultCredentials = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["useDefaultCredentials"]);
                        smtp_Client.UseDefaultCredentials = useDefaultCredentials;
                        smtp_Client.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                        smtp_Client.Send(mail);
                        Response.Write("<script>alert('sent')</script>");
                        ModelState.Clear();
                        TempData["result"] = "true";
                        Response.Write("<script>alert('email sent')</script>");
                        Console.WriteLine("sent");
                        return RedirectToAction("ContactRequestReply", "Admin");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occured while sending email : {ex.Message}");
                }
            }

            List<Contact> emailList = _context.Contacts.ToList();
            SelectList emailSelectList = new SelectList(emailList, "Email", "Email");
            ViewBag.listOfEmail = emailSelectList;
            var replyModel = new EmailReplyModel();
            return View(replyModel);
        }
        //GET : FAQ
        public ActionResult FaqRecords()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var faqData = _context.FAQs.ToList();
            return View(faqData);
        }
        //GET : FAQ Insertion
        public ActionResult FaqRecordInsertion()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //POST : FAQ Insertion
        [HttpPost]
        public ActionResult FaqRecordInsertion(FaqViewModel faqFormData)
        {
            if (ModelState.IsValid)
            {
                var faqdbmodel =new FAQ{
                    Questions=faqFormData.Questions,
                    Answer=faqFormData.Answer
                };
                _context.FAQs.Add(faqdbmodel);
                _context.SaveChanges();
                TempData["result"] = "true";
                ModelState.Clear();
                return RedirectToAction("FaqRecordInsertion", "Admin");
            }
            return View();
        }
        //GET : News
        public ActionResult NewsRecord()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var newsdata = _context.News.ToList();
            return View(newsdata);
        }
        //GET : News
        public ActionResult NewsInsertion()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //POST : News
        [HttpPost]
        public ActionResult NewsInsertion(NewsViewModel newsFormData)
        {
            if (ModelState.IsValid)
            {
                newsFormData.CurrentDateTime = DateTime.Now.ToString();
                var newsdbmodel = new News
                {
                    Title=newsFormData.Title,
                    Detail=newsFormData.Detail,
                    CurrentDateTime=newsFormData.CurrentDateTime
                };
                _context.News.Add(newsdbmodel);
                _context.SaveChanges();
                TempData["result"] = "true";
                ModelState.Clear();
                return RedirectToAction("NewsInsertion", "Admin");
            }
            return View();
        }
        //GET : Status home page
        public ActionResult Status()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //GET : crime status 
        public ActionResult UpdateCrimeComplainStatus()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var data = _context.CrimeComplains.ToList();
            return View(data);
        }
        //GET : Crime Status Edit
        public ActionResult CrimeStatusEdit(int id)
        {
            if (Session["adminmail"] == null)
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
            var preFetchData = _context.CrimeComplains
                .Where(model => model.Id == id)
                .Select(model => new CrimeComplainViewModel
                {
                    Username = model.Username,
                    PoliceStationName = model.PoliceStationName,
                    CrimeType = model.CrimeType,
                    InvolvedPersons = model.InvolvedPersons,
                    Proofs = model.Proofs,
                    CrimeStation = model.CrimeStation,
                    CurrentDateTime = model.CurrentDateTime,
                    Status = model.Status
                })
                .FirstOrDefault();
            Session["image"] = preFetchData.Proofs.ToString();
            return View(preFetchData);
        }
        //Post
        [HttpPost]
        public ActionResult CrimeStatusEdit(CrimeComplainViewModel updateCrimeData)
        {
            if (ModelState.IsValid)
            {
                var existingCrimeComplain = _context.CrimeComplains.Find(updateCrimeData.Id);
                if(existingCrimeComplain != null)
                {
                    existingCrimeComplain.Status = updateCrimeData.Status;
                    _context.SaveChanges();
                }
                return RedirectToAction("UpdateCrimeComplainStatus", "Admin");
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
        //GET
        public ActionResult UpdateGeneralComplainStatus()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var data = _context.GeneralComplains.ToList();
            return View(data);
        }
        //GET:General Complain
        public ActionResult GeneralStatusEdit(int id)
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
                })
                .ToList();
            var preFetchData = _context.GeneralComplains
                .Where(model => model.Id == id)
                .Select(model => new GeneralComplainViewModel
                {
                    Username = model.Username,
                    PoliceStationName = model.PoliceStationName,
                    Subject = model.Subject,
                    Details = model.Details,
                    InvolvedPersons = model.InvolvedPersons,
                    CurrentDateTime = model.CurrentDateTime,
                    Status = model.Status
                })
                .FirstOrDefault();
            ViewBag.OptionList = options;
            return View(preFetchData);
        }
        //POST
        [HttpPost]
        public ActionResult GeneralStatusEdit(GeneralComplainViewModel generalFormData)
        {
            if (ModelState.IsValid)
            {
                var existingGeneralComplain = _context.GeneralComplains.Find(generalFormData.Id);
                if (existingGeneralComplain != null)
                {
                    existingGeneralComplain.Status = generalFormData.Status;
                    _context.SaveChanges();
                }
                return RedirectToAction("UpdateGeneralComplainStatus", "Admin");
            }
            List<SelectListItem> options = _context.PoliceStations.Select(
                x => new SelectListItem
                {
                    Value = x.PoliceStationName.ToString(),
                    Text = x.PoliceStationName
                })
                .ToList();
            ViewBag.OptionList = options;
            return View();
        }
    }
}