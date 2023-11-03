using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContinentalTestDb.Data;
using Models.ContinentalModels;
using ContinentalTestDb.Services;
using Newtonsoft.Json;

namespace ContinentalTestDb.Controllers
{
    public class CoordinatorsController : Controller
    {
        private readonly ContinentalTestDbContext _context;

        public CoordinatorsController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Coordinators.Include(c => c.Worker);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId")] Coordinator coordinator)
        {
            var w = _context.Workers.SingleOrDefault(w => w.Id == coordinator.WorkerId);
            if (w != null)
            {
                _context.Add(coordinator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", coordinator.WorkerId);
            return View(coordinator);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Coordinators == null)
            {
                return NotFound();
            }

            var coordinator = await _context.Coordinators.FindAsync(id);
            if (coordinator == null)
            {
                return NotFound();
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", coordinator.WorkerId);
            return View(coordinator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkerId")] Coordinator coordinator)
        {
            if (id != coordinator.Id)
            {
                return NotFound();
            }

            var w = _context.Workers.SingleOrDefault(w => w.Id == coordinator.WorkerId);
            if (w != null)
            {
                try
                {   
                    _context.Update(coordinator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinatorExists(coordinator.Id))
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
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", coordinator.WorkerId);
            return View(coordinator);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coordinators == null)
            {
                return NotFound();
            }

            var coordinator = await _context.Coordinators
                .Include(c => c.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinator == null)
            {
                return NotFound();
            }

            return View(coordinator);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coordinators == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Coordinators'  is null.");
            }
            var coordinator = await _context.Coordinators.FindAsync(id);
            if (coordinator != null)
            {
                _context.Coordinators.Remove(coordinator);
            }        
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinatorExists(int id)
        {
          return _context.Coordinators.Any(e => e.Id == id);
        }
    }
}
