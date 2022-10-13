using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    [Authorize(Roles = "Systems Administrator")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.Users
                .Include(h => h.Status)
                .ToListAsync();

            return View(applicationDbContext);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.Include(h => h.Status)
                        .Where(h => h.Id == id)
                        .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", user.StatusId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email,StatusId,RoleId,CreatedAt,LastUpdatedAt")] ApplicationUser applicationUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (ModelState.IsValid)
            {
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.Email = applicationUser.Email;
                user.StatusId = applicationUser.StatusId;
                user.RoleId = applicationUser.RoleId;
                user.CreatedAt = applicationUser.CreatedAt;
                user.LastUpdatedAt = DateTime.Now;

                var response = await _userManager.UpdateAsync(applicationUser);
                if (response.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }

            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", applicationUser.StatusId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", applicationUser.RoleId);
            return View(applicationUser);
        }
    }
}
