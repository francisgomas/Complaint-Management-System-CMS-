using CMS.Data;
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
        public async Task<IActionResult> Index()
        {
            ViewData["HealthFacilities"] = new SelectList(_context.HealthFacility.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["ComplaintReasons"] = new SelectList(_context.ComplaintReason.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["Hospitals"] = new SelectList(_context.Hospital.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["HealthCenters"] = new SelectList(_context.HealthCenter.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["NursingStations"] = new SelectList(_context.NursingStation.Where(g => g.StatusId == 1), "Id", "Name");
            ViewData["Countries"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["Gender"] = new SelectList(_context.Gender, "Id", "Name");

            return View();
        }
    }
}
