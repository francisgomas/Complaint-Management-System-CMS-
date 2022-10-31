using CMS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
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
