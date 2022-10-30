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
        public async Task<IActionResult> Index()
        {
            ViewData["Created"] = _context.ComplaintForms.Count(x => x.FormStatusId == 1);
            ViewData["Archived"] = _context.ComplaintForms.Count(x => x.FormStatusId == 6);
            ViewData["Deleted"] = _context.ComplaintForms.Count(x => x.FormStatusId == 7);
            ViewData["Users"] = _context.Users.Count();
            var notifications = _context.Notification
                                .Include(c => c.ComplaintForm.ComplainantDetails)
                                .OrderBy(c => c.StatusId)
                                .ThenByDescending(c => c.LastUpdatedOn)
                                .ToList();

            if (!User.IsInRole("Systems Administrator"))
            {
                var user = await _userManager.GetUserAsync(User);
                notifications = _context.Notification
                                .Include(c => c.ComplaintForm.ComplainantDetails)
                                .Where(c => c.UserId == user.Id)
                                .OrderBy(c => c.StatusId)
                                .ThenByDescending(c => c.LastUpdatedOn)
                                .ToList();
            }

            return View(notifications);
        }
    }
}
