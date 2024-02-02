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
        readonly CTSEntitiesClass _context = new CTSEntitiesClass();
        //Logout:GET
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        //Index:GET
        public ActionResult Index()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //RecordRoom:GET
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
        //PoliceStationEntry:GET
        public ActionResult PoliceStationEntry()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var prefectchData = _context.PoliceStations.ToList();
            return View(prefectchData);
        }
        //PoliceStationEntryInsertion:GET
        public ActionResult PoliceStationEntryInsertion()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //PoliceStationEntryInsertion:POST
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
                        //mapping server image folder along with filename
                        actualFilename = Path.Combine(Server.MapPath("~/Uploads/PoliceStationImages/"), actualFilename);
                        //saving the file into folder
                        PoliceformData.ImageFile.SaveAs(actualFilename);
                        //adding data in db
                        var policedbmodel = new PoliceStation
                        {
                            ChowkiIncharge = PoliceformData.ChowkiIncharge,
                            PoliceStationName = PoliceformData.PoliceStationName,
                            CUGNumberOne = PoliceformData.CUGNumberOne,
                            CUGNumberSecond = PoliceformData.CUGNumberSecond,
                            Agent = PoliceformData.Agent,
                            AgentPhone = PoliceformData.AgentPhone,
                            PoliceStationImage = PoliceformData.PoliceStationImage,
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
        //ContactRequests:GET
        public ActionResult ContactRequests()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var contactData = _context.Contacts.ToList();
            return View(contactData);
        }
        //ContactRequestReply:GET
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
        //ContactRequestReply:POST
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
        //FaqRecords:GET
        public ActionResult FaqRecords()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var faqData = _context.FAQs.ToList();
            return View(faqData);
        }
        //FaqRecordInsertion:GET
        public ActionResult FaqRecordInsertion()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //FaqRecordInsertion:POST
        [HttpPost]
        public ActionResult FaqRecordInsertion(FaqViewModel faqFormData)
        {
            if (ModelState.IsValid)
            {
                var faqdbmodel = new FAQ
                {
                    Questions = faqFormData.Questions,
                    Answer = faqFormData.Answer
                };
                _context.FAQs.Add(faqdbmodel);
                _context.SaveChanges();
                TempData["result"] = "true";
                ModelState.Clear();
                return RedirectToAction("FaqRecordInsertion", "Admin");
            }
            return View();
        }
        //NewsRecord:GET
        public ActionResult NewsRecord()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var newsdata = _context.News.ToList();
            return View(newsdata);
        }
        //NewsInsertion:GET
        public ActionResult NewsInsertion()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //NewsInsertion:POST
        [HttpPost]
        public ActionResult NewsInsertion(NewsViewModel newsFormData)
        {
            if (ModelState.IsValid)
            {
                newsFormData.CurrentDateTime = DateTime.Now.ToString();
                var newsdbmodel = new News
                {
                    Title = newsFormData.Title,
                    Detail = newsFormData.Detail,
                    CurrentDateTime = newsFormData.CurrentDateTime
                };
                _context.News.Add(newsdbmodel);
                _context.SaveChanges();
                TempData["result"] = "true";
                ModelState.Clear();
                return RedirectToAction("NewsInsertion", "Admin");
            }
            return View();
        }
        
        //UpdateCrimeComplainStatus:GET
        public ActionResult UpdateCrimeComplainStatus()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var data = _context.CrimeComplains.ToList();
            return View(data);
        }
        //CrimeStatusEdit:GET
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
            return View(preFetchData);
        }
        //CrimeStatusEdit:Post
        [HttpPost]
        public ActionResult CrimeStatusEdit(CrimeComplainViewModel updateCrimeData)
        {
            if (ModelState.IsValid)
            {
                var existingCrimeComplain = _context.CrimeComplains.Find(updateCrimeData.Id);
                if (existingCrimeComplain != null)
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
        //UpdateGeneralComplainStatus:GET
        public ActionResult UpdateGeneralComplainStatus()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var data = _context.GeneralComplains.ToList();
            return View(data);
        }
        //GeneralStatusEdit:GET
        public ActionResult GeneralStatusEdit(int id)
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
        //GeneralStatusEdit:POST
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
        //General Details
        public ActionResult GeneralDetails(int id)
        {
            ViewBag.output = _context.GeneralComplains.Where(model => model.Id == id).FirstOrDefault();
            return View();
        }
        //UpdateMissingPersonStatus:GET
        public ActionResult UpdateMissingPersonStatus()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var data = _context.MissingPersons.ToList();
            return View(data);
        }
        //PersonStatusEdit:GET
        public ActionResult PersonStatusEdit(int id)
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

            var preFetchData = _context.MissingPersons
                .Where(model => model.Id == id)
                .Select(model => new MissingPersonViewModel
                {
                    Username = model.Username,
                    PoliceStationName = model.PoliceStationName,
                    Person = model.Person,
                    PersonPhone = model.PersonPhone,
                    LastLocation = model.LastLocation,
                    PersonEmail = model.PersonEmail,
                    Ransom = model.Ransom,
                    Details = model.Details,
                    PersonImage = model.PersonImage,
                    CurrentDateTime = model.CurrentDateTime,
                    Status = model.Status
                })
                .FirstOrDefault();
            ViewBag.OptionList = options;
            return View(preFetchData);
        }
        //PersonStatusEdit:POST
        [HttpPost]
        public ActionResult PersonStatusEdit(MissingPersonViewModel personFormData)
        {
            if (ModelState.IsValid)
            {
                var existingPerson = _context.MissingPersons.Find(personFormData.Id);
                if (existingPerson != null)
                {
                    existingPerson.Status = personFormData.Status;
                    _context.SaveChanges();
                }
                return RedirectToAction("UpdateMissingPersonStatus", "Admin");
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
        //Person Details
        public ActionResult PersonDetails(int id)
        {
            ViewBag.output = _context.MissingPersons.Where(model => model.Id == id).FirstOrDefault();
            return View();
        }
        //UpdateMissingValuableStatus:GET
        public ActionResult UpdateMissingValuableStatus()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var data = _context.MissingValuables.ToList();
            return View(data);
        }
        //ValuableStatusEdit:GET
        public ActionResult ValuableStatusEdit(int id)
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var preFetchData = _context.MissingValuables
                .Where(model => model.Id == id)
                .Select(model => new MissingValuableViewModel
                {
                    Username = model.Username,
                    PoliceStationName = model.PoliceStationName,
                    ValuableType = model.ValuableType,
                    ValuableCost = model.ValuableCost,
                    Suspect = model.Suspect,
                    ReciptImage = model.ReciptImage,
                    Details = model.Details,
                    CurrentDateTime = model.CurrentDateTime,
                    Status = model.Status
                })
                .FirstOrDefault();
            List<SelectListItem> options = _context.PoliceStations.Select(
                 x => new SelectListItem
                 {
                     Value = x.PoliceStationName.ToString(),
                     Text = x.PoliceStationName
                 }).ToList();

            ViewBag.OptionList = options;
            return View(preFetchData);
        }
        //ValuableStatusEdit:POST
        [HttpPost]
        public ActionResult ValuableStatusEdit(MissingValuableViewModel valuableFormData)
        {
            if (ModelState.IsValid)
            {
                var existingValuable = _context.MissingValuables.Find(valuableFormData.Id);
                if (existingValuable != null)
                {
                    existingValuable.Status = valuableFormData.Status;
                    _context.SaveChanges();
                }
                return RedirectToAction("UpdateMissingValuableStatus", "Admin");
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
        //Valuables Details
        public ActionResult ValuableDetails(int id)
        {
            ViewBag.output = _context.MissingValuables.Where(model => model.Id == id).FirstOrDefault();
            return View();
        }
        //EventUpload:get
        public ActionResult EventUpload()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //EventUpload:POST
        [HttpPost]
        public ActionResult EventUpload(EventViewModel eventFormData)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in eventFormData.ImageFiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        string fullnameWithFolder = "~/Uploads/EventImages/" + Path.GetFileName(file.FileName);
                        var pathOfImage = Path.Combine(Server.MapPath("~/Uploads/EventImages"), filename);
                        file.SaveAs(pathOfImage);
                        eventFormData.EventImage = fullnameWithFolder;
                        eventFormData.CurrentDateTime = DateTime.Now.ToString();
                        var eventdbmodel = new Event
                        {
                            EventTitle = eventFormData.EventTitle,
                            EventDateTime = eventFormData.EventDateTime,
                            CurrentDateTime = eventFormData.CurrentDateTime,
                            EventImage = eventFormData.EventImage
                        };
                        _context.Events.Add(eventdbmodel);
                        _context.SaveChanges();
                    }
                }
                ModelState.Clear();
                TempData["message"] = "<script>alert('upload successful')</script>";
                return RedirectToAction("EventUpload", "Admin");
            }
            return View();
        }
        //CriminalUpload:GET
        public ActionResult CriminalUpload()
        {
            if (Session["adminmail"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        //CriminalUpload:POST
        [HttpPost]
        public ActionResult CriminalUpload(CriminalImageViewModel criminalFormData)
        {
            if (ModelState.IsValid)
            {
                if (criminalFormData.ImageFiles != null && criminalFormData.ImageFiles.ContentLength > 0)
                {
                    string actualFilename = Path.GetFileName(criminalFormData.ImageFiles.FileName);
                    criminalFormData.CriminalImage = "~/Uploads/CriminalImages/" + actualFilename;
                    actualFilename = Path.Combine(Server.MapPath("~/Uploads/CriminalImages/"), actualFilename);
                    criminalFormData.ImageFiles.SaveAs(actualFilename);
                    criminalFormData.CurrentDateTime = DateTime.Now.ToString();
                    var criminaldbmodel = new CriminalGallery
                    {
                        Name = criminalFormData.Name,
                        AffectedOrganisation = criminalFormData.AffectedOrganisation,
                        Reward = criminalFormData.Reward,
                        CriminalImage = criminalFormData.CriminalImage,
                        Details = criminalFormData.Details,
                        CurrentDateTime=criminalFormData.CurrentDateTime
                    };
                    _context.CriminalGalleries.Add(criminaldbmodel);
                    _context.SaveChanges();
                    ModelState.Clear();
                    TempData["message"] = "<script>alert('upload successful')</script>";
                    return RedirectToAction("CriminalUpload", "Admin");
                }
            }
            return View();
        }
    }
}