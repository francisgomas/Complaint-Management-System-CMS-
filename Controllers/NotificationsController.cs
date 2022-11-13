using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.Notification.Include(h => h.Status)
                                            .Include(h => h.ComplaintForm)
                                            .Include(h => h.ApplicationUser)
                                            .OrderBy(c => c.StatusId)
                                            .ThenByDescending(c => c.LastUpdatedOn)
                                            .ToListAsync();

            if (!User.IsInRole("Systems Administrator"))
            {
                var user = await _userManager.GetUserAsync(User);
                applicationDbContext = await _context.Notification.Include(h => h.Status)
                                            .Include(h => h.ComplaintForm)
                                            .Include(h => h.ApplicationUser)
                                            .Where(c => c.UserId == user.Id)
                                            .OrderBy(c => c.StatusId)
                                            .ThenByDescending(c => c.LastUpdatedOn)
                                            .ToListAsync();
            }
                                            
            return View(applicationDbContext);
        }

        [HttpGet]
        public async Task<IActionResult> CheckRedirect(Guid? id)
        {
            if (id == null || _context.Notification == null)
            {
                return NotFound();
            }

            var notification = await _context.Notification
                .Include(c => c.ComplaintForm)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Status)
                .Where(c => c.StatusId == 1)
                .FirstOrDefaultAsync(m => m.ComplaintFormId == id);

            if (notification != null)
            {
                notification.StatusId = 2;
                notification.LastUpdatedOn = DateTime.Now;
                _context.SaveChanges();
            }

            return RedirectToAction("Details", "ComplaintForms", new { id = id });
        }
    }
}
