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
    public class NursingStationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NursingStationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NursingStations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NursingStation.Include(n => n.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NursingStations/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name");
            return View();
        }

        // POST: NursingStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] NursingStation nursingStation)
        {
            if (ModelState.IsValid)
            {
                nursingStation.StatusId = 1;
                _context.Add(nursingStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", nursingStation.StatusId);
            return View(nursingStation);
        }

        // GET: NursingStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NursingStation == null)
            {
                return NotFound();
            }

            var nursingStation = await _context.NursingStation.FindAsync(id);
            if (nursingStation == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", nursingStation.StatusId);
            return View(nursingStation);
        }

        // POST: NursingStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StatusId,CreatedAt,LastUpdatedAt")] NursingStation nursingStation)
        {
            if (id != nursingStation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nursingStation.LastUpdatedAt = DateTime.Now;
                    _context.Update(nursingStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NursingStationExists(nursingStation.Id))
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
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", nursingStation.StatusId);
            return View(nursingStation);
        }

        private bool NursingStationExists(int id)
        {
          return _context.NursingStation.Any(e => e.Id == id);
        }
    }
}
