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
            //ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", healthCenter.StatusId);
            return View();
        }
    }
}
