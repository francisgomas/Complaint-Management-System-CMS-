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
    [Authorize]
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
              return View(await _context.HealthFacility.ToListAsync());
        }

        // GET: HealthFacilities/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,CreatedAt,LastUpdatedAt")] HealthFacility healthFacility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(healthFacility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(healthFacility);
        }

        // POST: HealthFacilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,CreatedAt,LastUpdatedAt")] HealthFacility healthFacility)
        {
            if (id != healthFacility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            return View(healthFacility);
        }

        // GET: HealthFacilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HealthFacility == null)
            {
                return NotFound();
            }

            var healthFacility = await _context.HealthFacility
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthFacility == null)
            {
                return NotFound();
            }

            return View(healthFacility);
        }

        // POST: HealthFacilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HealthFacility == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HealthFacility'  is null.");
            }
            var healthFacility = await _context.HealthFacility.FindAsync(id);
            if (healthFacility != null)
            {
                _context.HealthFacility.Remove(healthFacility);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthFacilityExists(int id)
        {
          return _context.HealthFacility.Any(e => e.Id == id);
        }
    }
}
