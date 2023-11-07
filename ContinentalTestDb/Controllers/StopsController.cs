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
    public class StopsController : Controller
    {
        private readonly ContinentalTestDbContext _context;

        public StopsController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Stops.Include(s => s.Line).Include(s => s.Reason);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name");
            ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Planned,InitialDate,EndDate,Duration,Shift,LineId,ReasonId")] Stop stop)
        {
            var line = await _context.Lines.SingleOrDefaultAsync(l => l.Id == stop.LineId);
            if (line == null)
            {
                ModelState.AddModelError("LineId", "LineId inválido. Insira um LineId válido.");
                ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", stop.LineId);
                ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description", stop.ReasonId);
                return View(stop);
            }
            if(stop.InitialDate >= stop.EndDate)
            {
                ModelState.AddModelError("InitialDate", "InitialDate inválido. Tem de inserir um initial date menor que um EndDate.");
                ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", stop.LineId);
                ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description", stop.ReasonId);
                return View(stop);
            }
            stop.Line = line;
            //ver se a reason existe 
            var reason = await _context.Reasons.SingleOrDefaultAsync(r => r.Id == stop.ReasonId);
            if (reason != null)
            {
                stop.Reason = reason;
            }
            //adicionar a duration
            TimeSpan duration = stop.EndDate - stop.InitialDate;
            if (duration.TotalHours <= 24)
            {
                stop.Duration = duration;
            }
            else
            {
                stop.Duration = new TimeSpan(23, 59, 59);
            }
            _context.Add(stop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops.FindAsync(id);
            if (stop == null)
            {
                return NotFound();
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", stop.LineId);
            ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description", stop.ReasonId);
            return View(stop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Planned,InitialDate,EndDate,Duration,Shift,LineId,ReasonId")] Stop stop)
        {
            if (id != stop.Id)
            {
                return NotFound();
            }
            var line = await _context.Lines.SingleOrDefaultAsync(l => l.Id == stop.LineId);
            if (line == null)
            {
                ModelState.AddModelError("LineId", "LineId inválido. Insira um LineId válido.");
                ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", stop.LineId);
                ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description", stop.ReasonId);
                return View(stop);
            }
            if (stop.InitialDate >= stop.EndDate)
            {
                ModelState.AddModelError("InitialDate", "InitialDate inválido. Tem de inserir um initial date menor que um EndDate.");
                ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", stop.LineId);
                ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description", stop.ReasonId);
                return View(stop);
            }
            try
            {
                stop.Line = line;
                //ver se a reason existe 
                var reason = await _context.Reasons.SingleOrDefaultAsync(r => r.Id == stop.ReasonId);
                if (reason != null)
                {
                    stop.Reason = reason;
                }
                //adicionar a duration
                TimeSpan duration = stop.EndDate - stop.InitialDate;
                if (duration.TotalHours <= 24)
                {
                    stop.Duration = duration;
                }
                else
                {
                    stop.Duration = new TimeSpan(23, 59, 59);
                }
                _context.Update(stop);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopExists(stop.Id))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stops == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops
                .Include(s => s.Line)
                .Include(s => s.Reason)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stop == null)
            {
                return NotFound();
            }

            return View(stop);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stops == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Stops'  is null.");
            }
            var stop = await _context.Stops.FindAsync(id);
            if (stop != null)
            {
                _context.Stops.Remove(stop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StopExists(int id)
        {
          return _context.Stops.Any(e => e.Id == id);
        }
    }
}
