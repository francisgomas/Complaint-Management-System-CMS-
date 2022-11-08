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
using CMS.Services;

namespace CMS.Controllers
{
    public class ComplaintFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ComplaintFormsController(ApplicationDbContext context, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailService = emailService;
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
            var chkStatus = await _context.FormStatus.FindAsync(id);
            if (chkStatus != null)
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

            if (complaintForm.FileName != "")
            {
                complaintForm.Files = complaintForm.FileName.Split("|");
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
                ViewData["Users"] = _context.Users.Where(c => c.RoleId == "Section Head").ToList();
            }
            else
            {
                ViewData["Users"] = _context.Users.Where(c => c.RoleId == "Customer Service Head").ToList();
            }
        }

        public async Task<IActionResult> RespondToComplainant(Guid? id)
        {
            var cForm = await _context.ComplaintForms
                                .Include(c => c.ComplaintDetails)
                                .Include(c => c.ComplainantDetails)
                                .FirstOrDefaultAsync(c => c.Id == id);
            if (cForm == null)
            {
                return NotFound();
            }

            return View("Response", cForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RespondToComplainant(ComplaintForm complaintForm)
        {
            if (ModelState.IsValid)
            {
                var complaintsForm = await _context.ComplaintForms
                                .Include(c => c.ComplaintDetails)
                                .Include(c => c.ComplainantDetails)
                                .FirstOrDefaultAsync(c => c.Id == complaintForm.Id);

                if (complaintsForm != null)
                {
                    complaintsForm.Comments = complaintForm.Comments;
                    if (complaintsForm.FormStatusId == 5)
                    {
                        complaintsForm.FormStatusId = 4;
                    }
                    else
                    {
                        complaintsForm.FormStatusId = 3;
                    }
                    complaintsForm.UpdatedAt = DateTime.Now;
                    _context.Update(complaintsForm);
                    await _context.SaveChangesAsync();

                    var notification = new Notification();
                    notification.ComplaintFormId = complaintForm.Id;
                    notification.Description = "Assigned to complainant";
                    notification.LongDescription = complaintForm.Comments;

                    _context.Add(notification);
                    await _context.SaveChangesAsync();

                    //send email
                    var emailDetails = new EmailData();
                    emailDetails.EmailToId = complaintsForm.ComplainantDetails.Email;
                    emailDetails.EmailToName = complaintsForm.ComplainantDetails.FirstName + " " + complaintsForm.ComplainantDetails.LastName;
                    emailDetails.EmailSubject = "Complaint Response - Ministry of Health & Medical Services";
                    emailDetails.EmailBody = "Thank you for your patience. Please find response for your application with tracking number " + complaintsForm.TrackingId
                        + "<p><strong>" + complaintsForm.Comments + "</strong></p>";
                    if (complaintsForm.FormStatusId == 4)
                    {
                        emailDetails.EmailBody = "Thank you for your patience. Please find response from the Permanent Secretary(PS) for your application with tracking number " + complaintsForm.TrackingId
                                + "<p><strong>" + complaintsForm.Comments + "</strong></p>";
                    }
                       
                    await _emailService.SendEmail(emailDetails);
                }

                return RedirectToAction("Index", new { id = 1 });
            }

            GetUsers();
            return View(complaintForm);
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
                var complaintsForm = await _context.ComplaintForms
                                .Include(c => c.ComplaintDetails)
                                .Include(c => c.ComplainantDetails)
                                .FirstOrDefaultAsync(c => c.Id == complaintForm.Id);

                if (complaintsForm != null)
                {
                    complaintsForm.Comments = complaintForm.Comments;
                    complaintsForm.AssignedToId = complaintForm.AssignedToId;
                    complaintsForm.FormStatusId = 2;
                    complaintsForm.UpdatedAt = DateTime.Now;
                    _context.Update(complaintsForm);
                    await _context.SaveChangesAsync();
                }

                var user = await _userManager.FindByIdAsync(complaintForm.AssignedToId);
                if (user != null)
                {
                    var notification = new Notification();
                    notification.StatusId = 1;
                    notification.ComplaintFormId = complaintForm.Id;
                    notification.LongDescription = complaintForm.Comments;
                    notification.Description = $"Assigned to {user.FirstName + " " + user.LastName}";
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
