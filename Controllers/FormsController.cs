using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FormsController(ApplicationDbContext context)
        {
            _context = context; 
        }

        private void GetAllData()
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
            GetAllData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ComplaintForm complaintForm)
        {
            GetAllData();
            if (ModelState.IsValid)
            {
                complaintForm.TrackingId = GenerateTrackingId();
                _context.Add(complaintForm);
                await _context.SaveChangesAsync();

                ViewBag.SuccessMessage = "Tracking Number: " + complaintForm.TrackingId;
                ModelState.Clear();
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
                form = await _context.ComplaintForms.FirstOrDefaultAsync(a => a.TrackingId == applicationTracking.TrackingId);
                if (form != null)
                {
                    return Ok("Form is currently in " + form.FormStatus.FormStatusName + ". Last update on " + form.UpdatedAt);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Enter required fields");
            }
            
        }
    }

    
}
