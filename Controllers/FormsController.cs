using CMS.Data;
using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IEmailService _emailService = null;
        public FormsController(ApplicationDbContext context, IEmailService emailService)
        {
            _emailService = emailService;
            _context = context; 
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ComplaintForm complaintForm)
        {
            await GetAllData();
            if (ModelState.IsValid)
            {
                complaintForm.TrackingId = GenerateTrackingId();
                _context.Add(complaintForm);
                await _context.SaveChangesAsync();

                ViewBag.SuccessMessage = "Tracking Number: " + complaintForm.TrackingId;

                //send email
                var emailDetails = new EmailData();
                emailDetails.EmailToId = complaintForm.ComplainantDetails.Email;
                emailDetails.EmailToName = complaintForm.ComplainantDetails.FirstName + " " + complaintForm.ComplainantDetails.LastName;
                emailDetails.EmailSubject = "Confirmation Email - Ministry of Health & Medical Services";
                emailDetails.EmailBody = "Thank you for using our CMS (Complaint Management System)! We have received your submission " +
                    "and it will be processed shortly. Your application tracking number is " + complaintForm.TrackingId + ". Please use the " +
                    "<strong>Track my application</strong> feature to determine the progress of your complaint!";

                ModelState.Clear();
                await _emailService.SendEmail(emailDetails);
                return View();
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
                    return Json(new
                    {
                        message = "Complaint Form is in " + form.FormStatus.FormStatusName + " status. Last updated on " + form.UpdatedAt,
                        result = true
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
