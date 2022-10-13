using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    [Authorize(Roles = "Systems Administrator")]
    public class HealthFacilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HealthFacilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HealthFacilities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HealthFacility.Include(h => h.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HealthFacilities/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name");
            return View();
        }

        // POST: HealthFacilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] HealthFacility healthFacility)
        {
            if (ModelState.IsValid)
            {
                healthFacility.StatusId = 1;
                _context.Add(healthFacility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", healthFacility.StatusId);
            return View(healthFacility);
        }

        // GET: HealthFacilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HealthFacility == null)
            {
                return NotFound();
            }

            var healthFacility = await _context.HealthFacility.FindAsync(id);
            if (healthFacility == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", healthFacility.StatusId);
            return View(healthFacility);
        }

        // POST: HealthFacilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] HealthFacility healthFacility)
        {
            if (id != healthFacility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    healthFacility.LastUpdatedAt = DateTime.Now;
                    _context.Update(healthFacility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthFacilityExists(healthFacility.Id))
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
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", healthFacility.StatusId);
            return View(healthFacility);
        }

        private bool HealthFacilityExists(int id)
        {
          return _context.HealthFacility.Any(e => e.Id == id);
        }
    }
}
