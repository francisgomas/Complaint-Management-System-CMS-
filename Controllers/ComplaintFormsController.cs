using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Identity;

namespace CMS.Controllers
{
    public class ComplaintFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ComplaintFormsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task setTitle(int id = 0)
        {
            ViewData["Title"] = "Complaint Forms";
            var formStatus = _context.FormStatus.Where(a => a.Id == id).FirstOrDefault();
            if (formStatus != null)
            {
                ViewData["Title"] += " - " + formStatus.FormStatusName;
            }

            return Task.CompletedTask;
        }

        // GET: ComplaintForms
        public async Task<IActionResult> Index(int id)
        {
            if (id == 1 || id == 6 || id == 7)
            {
                var applicationDbContext = _context.ComplaintForms.Include(c => c.ComplainantDetails)
                .Include(c => c.ComplaintDetails)
                .Include(c => c.FormStatus)
                .Include(c => c.ApplicationUser)
                .Where(c => c.FormStatusId == id);

                if (!User.IsInRole("Systems Administrator"))
                {
                    var user = await _userManager.GetUserAsync(User);
                    applicationDbContext = applicationDbContext.Where(c => c.AssignedToId == user.Id);
                }

                await setTitle(id);
                return View(await applicationDbContext.OrderByDescending(c => c.UpdatedAt).ToListAsync());
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(Guid complaintformid, int statusid)
        {
            var form = await _context.ComplaintForms
                .Include(c => c.ComplainantDetails)
                .Include(c => c.ComplaintDetails)
                .Include(c => c.FormStatus)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == complaintformid);

            var statuschk = await _context.FormStatus.FirstOrDefaultAsync(c => c.Id == statusid);

            if (form != null && statuschk != null)
            {
                var tempstat = form.FormStatusId;
                form.FormStatusId = statuschk.Id;
                form.UpdatedAt = DateTime.Now;
                _context.Update(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = tempstat });
            }
            return NotFound();
        }


        // GET: ComplaintForms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ComplaintForms == null)
            {
                return NotFound();
            }

            var complaintForm = await _context.ComplaintForms
                .Include(c => c.ComplainantDetails)
                .Include(c => c.ComplainantDetails.Gender)
                .Include(c => c.ComplainantDetails.Country)
                .Include(c => c.ComplaintDetails)
                .Include(c => c.ComplaintDetails.HealthFacility)
                .Include(c => c.ComplaintDetails.ComplaintReason)
                .Include(c => c.FormStatus)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (complaintForm == null)
            {
                return NotFound();
            }

            return View(complaintForm);
        }

        // GET: ComplaintForms/Logs/5
        public async Task<IActionResult> Logs(Guid? id)
        {
            if (id == null || _context.ComplaintForms == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notification
                .Include(c => c.Status)
                .Include(c => c.ApplicationUser)
                .Where(c => c.ComplaintFormId == id)
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();

            if (notifications == null)
            {
                return NotFound();
            }

            return View(notifications);
        }

        private async void GetUsers()
        {
            if (User.IsInRole("Systems Administrator"))
            {
                ViewData["Users"] = _context.Users.Where(c => c.Email != User.Identity.Name).ToList();
               
            }
            else if(User.IsInRole("Customer Service Head"))
            {
                ViewData["Users"] = await _userManager.GetUsersInRoleAsync("Section Head");
            }
            else

            {
                ViewData["Users"] = await _userManager.GetUsersInRoleAsync("Customer Service Head");
            }
        }

        // GET: ComplaintForms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var cForm = await _context.ComplaintForms
                                .Include(c => c.ComplaintDetails)
                                .Include(c => c.ComplainantDetails)
                                .FirstOrDefaultAsync(c => c.Id == id);
            if (cForm == null)
            {
                return NotFound();
            }

            GetUsers();
            return View(cForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ComplaintForm complaintForm)
        {
            if (ModelState.IsValid)
            {
                complaintForm.FormStatusId = 2;
                complaintForm.UpdatedAt = DateTime.Now;
                _context.Update(complaintForm);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByIdAsync(complaintForm.AssignedToId);
                if (user != null)
                {
                    var notification = new Notification();
                    notification.StatusId = 1;
                    notification.ComplaintFormId = complaintForm.Id;
                    notification.Description = $"Assigned to {user.FirstName + " " + user.LastName + " (" + user.Email + ")"}";
                    notification.UserId = complaintForm.AssignedToId;

                    _context.Add(notification);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction("Index", new {id = 1});
            }

            GetUsers();
            return View(complaintForm);
        }
    }
}
