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
    public class HealthCentersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HealthCentersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HealthCenters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HealthCenter.Include(h => h.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HealthCenters/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name");
            return View();
        }

        // POST: HealthCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] HealthCenter healthCenter)
        {
            if (ModelState.IsValid)
            {
                healthCenter.StatusId = 1;
                _context.Add(healthCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", healthCenter.StatusId);
            return View(healthCenter);
        }

        // GET: HealthCenters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HealthCenter == null)
            {
                return NotFound();
            }

            var healthCenter = await _context.HealthCenter.FindAsync(id);
            if (healthCenter == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", healthCenter.StatusId);
            return View(healthCenter);
        }

        // POST: HealthCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] HealthCenter healthCenter)
        {
            if (id != healthCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    healthCenter.LastUpdatedAt = DateTime.Now;
                    _context.Update(healthCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthCenterExists(healthCenter.Id))
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
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", healthCenter.StatusId);
            return View(healthCenter);
        }

        private bool HealthCenterExists(int id)
        {
          return _context.HealthCenter.Any(e => e.Id == id);
        }
    }
}
