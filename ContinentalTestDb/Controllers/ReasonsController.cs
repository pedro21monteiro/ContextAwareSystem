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
    public class ReasonsController : Controller
    {
        private readonly ContinentalTestDbContext _context;

        public ReasonsController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.Reasons.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Reason reason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reason);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(reason);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reasons == null)
            {
                return NotFound();
            }

            var reason = await _context.Reasons.FindAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            return View(reason);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Reason reason)
        {
            if (id != reason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReasonExists(reason.Id))
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
            return View(reason);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reasons == null)
            {
                return NotFound();
            }

            var reason = await _context.Reasons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reason == null)
            {
                return NotFound();
            }

            return View(reason);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reasons == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Reasons'  is null.");
            }
            var reason = await _context.Reasons.FindAsync(id);
            if (reason != null)
            {
                _context.Reasons.Remove(reason);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReasonExists(int id)
        {
          return _context.Reasons.Any(e => e.Id == id);
        }
    }
}
