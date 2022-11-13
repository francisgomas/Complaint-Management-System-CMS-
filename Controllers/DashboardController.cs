using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<int> customerhead = new List<int>();
            List<int> sectionhead = new List<int>();
            List<int> ps = new List<int>();

            ViewData["Users"] = _context.Users.Count();
            ViewData["HealthFacility"] = _context.HealthFacility.Count();
            ViewData["ComplaintReason"] = _context.ComplaintReason.Count();
            ViewData["Hospital"] = _context.Hospital.Count();
            ViewData["HealthCenter"] = _context.HealthCenter.Count();
            ViewData["NursingStation"] = _context.NursingStation.Count();

            for (int i = 1; i <= 7; i++)
            {
                customerhead.Add(_context.ComplaintForms
                                    .Where(x => x.ApplicationUser.RoleId == "Customer Service Head")
                                    .Count(x => x.FormStatusId == i));

                sectionhead.Add(_context.ComplaintForms
                                    .Where(x => x.ApplicationUser.RoleId == "Section Head")
                                    .Count(x => x.FormStatusId == i));

                ps.Add(_context.ComplaintForms
                                    .Where(x => x.ApplicationUser.RoleId == "Permanent Secretary")
                                    .Count(x => x.FormStatusId == i));
            }

            ViewBag.Customerhead = customerhead;
            ViewBag.Sectionhead = sectionhead;
            ViewBag.PS = ps;

            var notifications = _context.Notification
                                .Include(c => c.ComplaintForm.ComplainantDetails)
                                .OrderBy(c => c.StatusId)
                                .ThenByDescending(c => c.LastUpdatedOn)
                                .Take(10)
                                .ToList();

            if (!User.IsInRole("Systems Administrator"))
            {
                var user = await _userManager.GetUserAsync(User);
                notifications = _context.Notification
                                .Include(c => c.ComplaintForm.ComplainantDetails)
                                .Where(c => c.UserId == user.Id)
                                .OrderBy(c => c.StatusId)
                                .ThenByDescending(c => c.LastUpdatedOn)
                                .Take(10)
                                .ToList();
            }


            return View(notifications);
        }
    }
}
