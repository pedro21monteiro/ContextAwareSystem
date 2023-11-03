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
    public class LinesController : Controller
    {
        private readonly ContinentalTestDbContext _context;

        public LinesController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Lines.Include(l => l.Coordinator);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["CoordinatorId"] = new SelectList(_context.Coordinators, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Priority,CoordinatorId")] Line line)
        {
            var c = _context.Coordinators.SingleOrDefault(c => c.Id == line.CoordinatorId);
            if (c != null)
            {
                _context.Add(line);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinatorId"] = new SelectList(_context.Coordinators, "Id", "Id", line.CoordinatorId);
            return View(line);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lines == null)
            {
                return NotFound();
            }

            var line = await _context.Lines.FindAsync(id);
            if (line == null)
            {
                return NotFound();
            }
            ViewData["CoordinatorId"] = new SelectList(_context.Coordinators, "Id", "Id", line.CoordinatorId);
            return View(line);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Priority,CoordinatorId")] Line line)
        {
            if (id != line.Id)
            {
                return NotFound();
            }
            var c = _context.Coordinators.SingleOrDefault(c => c.Id == line.CoordinatorId);
            if (c != null)
            {
                try
                {   
                    _context.Update(line);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineExists(line.Id))
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
            ViewData["CoordinatorId"] = new SelectList(_context.Coordinators, "Id", "Id", line.CoordinatorId);
            return View(line);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lines == null)
            {
                return NotFound();
            }

            var line = await _context.Lines
                .Include(l => l.Coordinator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lines == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Lines'  is null.");
            }
            var line = await _context.Lines.FindAsync(id);
            if (line != null)
            {
                _context.Lines.Remove(line);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineExists(int id)
        {
          return _context.Lines.Any(e => e.Id == id);
        }
    }
}
