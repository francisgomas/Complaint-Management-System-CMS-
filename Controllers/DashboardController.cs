using CMS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Created"] = _context.ComplaintForms.Count(x => x.FormStatusId == 1);
            ViewData["Archived"] = _context.ComplaintForms.Count(x => x.FormStatusId == 6);
            ViewData["Deleted"] = _context.ComplaintForms.Count(x => x.FormStatusId == 7);
            ViewData["Users"] = _context.Users.Count();
            return View();
        }
    }
}
