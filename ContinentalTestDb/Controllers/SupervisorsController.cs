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
    public class SupervisorsController : Controller
    {
        private readonly ContinentalTestDbContext _context;

        public SupervisorsController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Supervisors.Include(s => s.Worker);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId")] Supervisor supervisor)
        {
            var w = _context.Workers.SingleOrDefault(w => w.Id == supervisor.WorkerId);
            if (w != null)
            {
                _context.Add(supervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", supervisor.WorkerId);
            return View(supervisor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supervisors == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", supervisor.WorkerId);
            return View(supervisor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkerId")] Supervisor supervisor)
        {
            if (id != supervisor.Id)
            {
                return NotFound();
            }
            var w = _context.Workers.SingleOrDefault(w => w.Id == supervisor.WorkerId);
            if (w != null)
            {         
                try
                {
                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisorExists(supervisor.Id))
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
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", supervisor.WorkerId);
            return View(supervisor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supervisors == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors
                .Include(s => s.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Supervisors == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Supervisors'  is null.");
            }
            var supervisor = await _context.Supervisors.FindAsync(id);
            if (supervisor != null)
            {
                _context.Supervisors.Remove(supervisor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupervisorExists(int id)
        {
          return _context.Supervisors.Any(e => e.Id == id);
        }
    }
}
