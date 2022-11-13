using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void GetData()
        {
            ViewData["HealthFacilities"] = new SelectList(_context.HealthFacility.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["ComplaintReasons"] = new SelectList(_context.ComplaintReason.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["Hospitals"] = new SelectList(_context.Hospital.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["HealthCenters"] = new SelectList(_context.HealthCenter.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["NursingStations"] = new SelectList(_context.NursingStation.Where(g => g.StatusId == 1), "Id", "Name");
        }

        public IActionResult Index()
        {
            //GetData();
            var report = new Report();
            report.TotalCount = _context.ComplaintForms.Count();
            report.HospitalCount = _context.ComplaintForms.Where(x => x.ComplaintDetails.HealthFacilityId == 5).Count();
            report.NursingStationCount = _context.ComplaintForms.Where(x => x.ComplaintDetails.HealthFacilityId == 6).Count();
            report.HealthCenterCount = _context.ComplaintForms.Where(x => x.ComplaintDetails.HealthFacilityId == 7).Count();
            report.OthersCount = _context.ComplaintForms.Where(x => x.ComplaintDetails.HealthFacilityId == 8).Count();
            report.CreatedCount = _context.ComplaintForms.Where(x => x.FormStatusId == 1).Count();
            report.InProcessingCount = _context.ComplaintForms.Where(x => x.FormStatusId == 2).Count();
            report.ReviewedCount = _context.ComplaintForms.Where(x => x.FormStatusId == 3).Count();
            report.ProcessedCount = _context.ComplaintForms.Where(x => x.FormStatusId == 4).Count();
            report.AwaitingPSCount = _context.ComplaintForms.Where(x => x.FormStatusId == 5).Count();
            report.ArchivedCount = _context.ComplaintForms.Where(x => x.FormStatusId == 6).Count();
            report.DeletedCount = _context.ComplaintForms.Where(x => x.FormStatusId == 7).Count();

            return View(report);
        }
    }
}
