using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index()
        {
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

            List<int> hospitals = new List<int>();
            List<int> healthcenters = new List<int>();
            List<int> nursingstations = new List<int>();

            var hospitalNames = _context.ComplaintDetails.Include(x => x.Hospital)
                                    .Where(x => x.HealthFacilityId == 5)
                                    .Where(x => x.HospitalId == x.Hospital.Id)
                                    .ToList();

            var healthCenters = _context.ComplaintDetails.Include(x => x.HealthCenter)
                                    .Where(x => x.HealthFacilityId == 6)
                                    .Where(x => x.HealthCenterId == x.HealthCenter.Id)
                                    .ToList();

            var nursingStations = _context.ComplaintDetails.Include(x => x.NursingStation)
                                    .Where(x => x.HealthFacilityId == 7)
                                    .Where(x => x.NursingStationId== x.NursingStation.Id)
                                    .ToList();

            foreach (var item in hospitalNames)
            {
                hospitals.Add(_context.ComplaintDetails.Count(x => x.HospitalId == item.HospitalId));
            }
            foreach (var item in healthCenters)
            {
                healthcenters.Add(_context.ComplaintDetails.Count(x => x.HealthCenterId == item.HealthCenterId));
            }
            foreach (var item in nursingStations)
            {
                nursingstations.Add(_context.ComplaintDetails.Count(x => x.NursingStationId == item.NursingStationId));
            }

            ViewBag.HospitalNames = hospitalNames;
            ViewBag.HospitalCounts = hospitals;
            ViewBag.HealthCenterNames = healthCenters;
            ViewBag.HealthCenterCount = healthcenters;
            ViewBag.NursingStationNames = nursingStations;
            ViewBag.NursingStationCount = nursingstations;

            return View(report);
        }
    }
}
