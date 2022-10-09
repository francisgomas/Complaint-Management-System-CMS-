using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;

namespace CMS.Controllers
{
    public class ComplaintFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComplaintForms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComplaintForms.Include(c => c.ComplainantDetails).Include(c => c.ComplaintDetails).Include(c => c.FormStatus);
            return View(await applicationDbContext.ToListAsync());
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
                .Include(c => c.ComplaintDetails)
                .Include(c => c.FormStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintForm == null)
            {
                return NotFound();
            }

            return View(complaintForm);
        }

        // GET: ComplaintForms/Create
        public IActionResult Create()
        {
            ViewData["ComplainantId"] = new SelectList(_context.ComplainantDetails, "Id", "ContactNumber");
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintDetails, "Id", "ComplaintBehalf");
            ViewData["FormStatusId"] = new SelectList(_context.FormStatus, "Id", "FormStatusName");
            return View();
        }

        // POST: ComplaintForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrackingId,ComplainantId,ComplaintId,FormStatusId,FileName,CreatedAt,UpdatedAt")] ComplaintForm complaintForm)
        {
            if (ModelState.IsValid)
            {
                complaintForm.Id = Guid.NewGuid();
                _context.Add(complaintForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplainantId"] = new SelectList(_context.ComplainantDetails, "Id", "ContactNumber", complaintForm.ComplainantId);
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintDetails, "Id", "ComplaintBehalf", complaintForm.ComplaintId);
            ViewData["FormStatusId"] = new SelectList(_context.FormStatus, "Id", "FormStatusName", complaintForm.FormStatusId);
            return View(complaintForm);
        }

        // GET: ComplaintForms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ComplaintForms == null)
            {
                return NotFound();
            }

            var complaintForm = await _context.ComplaintForms.FindAsync(id);
            if (complaintForm == null)
            {
                return NotFound();
            }
            ViewData["ComplainantId"] = new SelectList(_context.ComplainantDetails, "Id", "ContactNumber", complaintForm.ComplainantId);
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintDetails, "Id", "ComplaintBehalf", complaintForm.ComplaintId);
            ViewData["FormStatusId"] = new SelectList(_context.FormStatus, "Id", "FormStatusName", complaintForm.FormStatusId);
            return View(complaintForm);
        }

        // POST: ComplaintForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TrackingId,ComplainantId,ComplaintId,FormStatusId,FileName,CreatedAt,UpdatedAt")] ComplaintForm complaintForm)
        {
            if (id != complaintForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintFormExists(complaintForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplainantId"] = new SelectList(_context.ComplainantDetails, "Id", "ContactNumber", complaintForm.ComplainantId);
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintDetails, "Id", "ComplaintBehalf", complaintForm.ComplaintId);
            ViewData["FormStatusId"] = new SelectList(_context.FormStatus, "Id", "FormStatusName", complaintForm.FormStatusId);
            return View(complaintForm);
        }

        // GET: ComplaintForms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ComplaintForms == null)
            {
                return NotFound();
            }

            var complaintForm = await _context.ComplaintForms
                .Include(c => c.ComplainantDetails)
                .Include(c => c.ComplaintDetails)
                .Include(c => c.FormStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintForm == null)
            {
                return NotFound();
            }

            return View(complaintForm);
        }

        // POST: ComplaintForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ComplaintForms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ComplaintForms'  is null.");
            }
            var complaintForm = await _context.ComplaintForms.FindAsync(id);
            if (complaintForm != null)
            {
                _context.ComplaintForms.Remove(complaintForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintFormExists(Guid id)
        {
          return _context.ComplaintForms.Any(e => e.Id == id);
        }
    }
}
