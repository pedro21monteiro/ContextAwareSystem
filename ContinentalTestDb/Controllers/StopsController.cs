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
        private readonly RabbitMqService _rabbit;

        public StopsController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Stops
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Stops.Include(s => s.Line).Include(s => s.Reason);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Stops/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Stops/Create
        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name");
            ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description");
            return View();
        }

        // POST: Stops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Planned,InitialDate,EndDate,Duration,Shift,LineId,ReasonId")] Stop stop)
        {

            var l = _context.Lines.SingleOrDefault(l => l.Id == stop.LineId);
            var r = _context.Reasons.SingleOrDefault(r => r.Id == stop.ReasonId);
            if (l != null)
            {
                stop.Line = l;
                if(r != null)
                {
                    stop.Reason = r;
                }
                stop.LastUpdate = DateTime.Now;
                _context.Add(stop);
                await _context.SaveChangesAsync();
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(stop), "create.stop");
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", stop.LineId);
            ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description", stop.ReasonId);
            return View(stop);
        }

        // GET: Stops/Edit/5
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

        // POST: Stops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Planned,InitialDate,EndDate,Duration,Shift,LineId,ReasonId")] Stop stop)
        {
            if (id != stop.Id)
            {
                return NotFound();
            }

            var l = _context.Lines.SingleOrDefault(l => l.Id == stop.LineId);
            var r = _context.Reasons.SingleOrDefault(r => r.Id == stop.ReasonId);
            if (l != null)
            {             
                try
                {
                    stop.Line = l;
                    if (r != null)
                    {
                        stop.Reason = r;
                    }
                    stop.LastUpdate = DateTime.Now;
                    _context.Update(stop);
                    await _context.SaveChangesAsync();
                    await _rabbit.PublishMessage(JsonConvert.SerializeObject(stop), "update.stop");
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
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", stop.LineId);
            ViewData["ReasonId"] = new SelectList(_context.Reasons, "Id", "Description", stop.ReasonId);
            return View(stop);
        }

        // GET: Stops/Delete/5
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

        // POST: Stops/Delete/5
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
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(stop), "delete.stop");
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
