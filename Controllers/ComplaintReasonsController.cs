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
    public class ComplaintReasonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintReasonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComplaintReasons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComplaintReason.Include(c => c.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ComplaintReasons/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name");
            return View();
        }

        // POST: ComplaintReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] ComplaintReason complaintReason)
        {
            if (ModelState.IsValid)
            {
                complaintReason.StatusId = 1;
                _context.Add(complaintReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", complaintReason.StatusId);
            return View(complaintReason);
        }

        // GET: ComplaintReasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComplaintReason == null)
            {
                return NotFound();
            }

            var complaintReason = await _context.ComplaintReason.FindAsync(id);
            if (complaintReason == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", complaintReason.StatusId);
            return View(complaintReason);
        }

        // POST: ComplaintReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] ComplaintReason complaintReason)
        {
            if (id != complaintReason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    complaintReason.LastUpdatedAt = DateTime.Now;
                    _context.Update(complaintReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintReasonExists(complaintReason.Id))
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
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", complaintReason.StatusId);
            return View(complaintReason);
        }

        private bool ComplaintReasonExists(int id)
        {
          return _context.ComplaintReason.Any(e => e.Id == id);
        }
    }
}
