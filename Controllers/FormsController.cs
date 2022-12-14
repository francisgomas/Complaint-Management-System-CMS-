using CMS.Data;
using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http.Headers;

namespace CMS.Controllers
{
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IEmailService _emailService = null;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FormsController(ApplicationDbContext context, IEmailService emailService, RoleManager<IdentityRole> roleManager)
        {
            _emailService = emailService;
            _context = context; 
            _roleManager = roleManager;
        }

        private async Task GetAllData()
        {
            ViewData["HealthFacilities"] = new SelectList(_context.HealthFacility.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["ComplaintReasons"] = new SelectList(_context.ComplaintReason.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["Hospitals"] = new SelectList(_context.Hospital.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["HealthCenters"] = new SelectList(_context.HealthCenter.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["NursingStations"] = new SelectList(_context.NursingStation.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["Countries"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["Gender"] = new SelectList(_context.Gender, "Id", "Name");
        }

        public string GenerateTrackingId()
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < 12; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        public async Task<IActionResult> Index()
        {
            await GetAllData();
            var sMessage = TempData["SuccessMessage"];
            var eMessage = TempData["ErrorMessage"];

            if (sMessage != null)
            {
                ViewData["SuccessMessage"] = sMessage;
            }

            if (eMessage != null)
            {
                ViewData["ErrorMessage"] = eMessage;
            }

            return View();
        }

        public async Task<IActionResult> SubmitToPS()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitToPS(ComplaintPS complaintForm)
        {
            if (ModelState.IsValid)
            {
                var checkTrackingId = await _context.ComplaintForms
                            .FirstOrDefaultAsync(c => c.TrackingId == complaintForm.TrackingId);

                if (checkTrackingId == null)
                {
                    ViewBag.ErrorMessage = "Tracking number entered does not exist!";
                }
                else
                {
                    if (checkTrackingId.FormStatusId == 3)
                    {
                        var user = await _context.Users
                                            .Where(x => x.RoleId == "Permanent Secretary")
                                            .FirstOrDefaultAsync();

                        checkTrackingId.FormStatusId = 5;
                        checkTrackingId.Comments = complaintForm.Comments;
                        checkTrackingId.UpdatedAt = DateTime.Now;
                        checkTrackingId.AssignedToId = user.Id;
                        _context.Update(checkTrackingId);
                        await _context.SaveChangesAsync();

                        var notification = new Notification();
                        notification.ComplaintFormId = checkTrackingId.Id;
                        notification.Description = "Awaiting response from PS";
                        notification.LongDescription = complaintForm.Comments;
                        notification.UserId = user.Id;

                        _context.Add(notification);
                        await _context.SaveChangesAsync();

                        ViewBag.SuccessMessage = "Your application has been escalated to PS!";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Your application is either in review/processed/archived/deleted!";
                    }
                }
                ModelState.Clear();
                return View();
            }
            
            return View(complaintForm);
        }

        private async Task<string> FindRandomServiceHead()
        {
            var role = await _roleManager.FindByIdAsync("1");
            var user = await _context.Users.Where(x => x.RoleId == role.Name)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();

            if (user == null) return null;
            return user.Id;
        }

        private async Task AddNotification(Guid id, string desc, string userid)
        {
            var notification = new Notification();
            notification.ComplaintFormId = id;
            notification.Description = desc;
            notification.LongDescription = desc;
            notification.UserId = userid;

            _context.Add(notification);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ComplaintForm complaintForm, List<IFormFile> userfiles)
        {
            await GetAllData();
            if (ModelState.IsValid)
            {
                var randomUserId = await FindRandomServiceHead();
                complaintForm.TrackingId = GenerateTrackingId();
                complaintForm.AssignedToId = randomUserId;

                if (userfiles.Count > 0)
                {
                    foreach(var file in userfiles)
                    {
                        string filename = file.FileName;
                        filename = Path.GetFileName(Convert.ToString(Guid.NewGuid()) + "." + filename.Split('.').Last());  
                        Path.GetFileName(filename);
                        string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploads", filename);
                        var stream = new FileStream(uploadpath, FileMode.Create);
                        file.CopyTo(stream);

                        complaintForm.FileName += filename + "|";
                    }
                }

                _context.Add(complaintForm);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Tracking Number: " + complaintForm.TrackingId;

                    //send email
                    var emailDetails = new EmailData();
                    emailDetails.EmailToId = complaintForm.ComplainantDetails.Email;
                    emailDetails.EmailToName = complaintForm.ComplainantDetails.FirstName + " " + complaintForm.ComplainantDetails.LastName;
                    emailDetails.EmailSubject = "Confirmation Email - Ministry of Health & Medical Services";
                    emailDetails.EmailBody = "Thank you for using our CMS (Complaint Management System)! We have received your submission " +
                        "and it will be processed shortly. Your application tracking number is " + complaintForm.TrackingId + ". Please use the " +
                        "<strong>Track my application</strong> feature to determine the progress of your complaint!";

                    await _emailService.SendEmail(emailDetails);
                    await AddNotification(complaintForm.Id, "New complaint form submission!", randomUserId);
                }
                else
                {
                    TempData["ErrorMessage"] = "An unknown error has occured";
                }

                ModelState.Clear();
                return RedirectToAction("Index");
            }

            return View(complaintForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrackApp(Tracking applicationTracking)
        {
            if (ModelState.IsValid)
            {
                ComplaintForm form = new ComplaintForm();
                form = await _context.ComplaintForms
                    .Include(c => c.FormStatus)
                    .FirstOrDefaultAsync(a => a.TrackingId == applicationTracking.TrackingId);
                if (form != null)
                {
                    if (form.FormStatusId == 3 || form.FormStatusId == 4)
                    {
                        var notification = await _context.Notification.Where(x => x.ComplaintFormId == form.Id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                        return Json(new
                        {
                            message = "Complaint Form is in " + form.FormStatus.FormStatusName + " status. Last updated on " + form.UpdatedAt +
                                    "<br/><br/>Feedback: " + notification.LongDescription,
                            result = true,
                            status = form.FormStatusId
                        });
                    }
                    return Json(new
                    {
                        message = "Complaint Form is in " + form.FormStatus.FormStatusName + " status. Last updated on " + form.UpdatedAt,
                        result = true,
                        status = form.FormStatusId
                    });
                }
                else
                {
                    return Json(new
                    {
                        message = "Incorrect tracking number entered!",
                        result = false
                    });
                }
            }

            return Json(new
            {
                message = "Please enter tracking number",
                result = false
            });
        }
    }
}
