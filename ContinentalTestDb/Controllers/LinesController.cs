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
        private readonly RabbitMqService _rabbit;

        public LinesController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Lines
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Lines.Include(l => l.Coordinator);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Lines/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Lines/Create
        public IActionResult Create()
        {
            ViewData["CoordinatorId"] = new SelectList(_context.Coordinators, "Id", "Id");
            return View();
        }

        // POST: Lines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Priority,CoordinatorId")] Line line)
        {
            var c = _context.Coordinators.SingleOrDefault(c => c.Id == line.CoordinatorId);
            if (c != null)
            {
                line.LastUpdate = DateTime.Now;
                _context.Add(line);
                await _context.SaveChangesAsync();
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(line), "create.line");
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinatorId"] = new SelectList(_context.Coordinators, "Id", "Id", line.CoordinatorId);
            return View(line);
        }

        // GET: Lines/Edit/5
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

        // POST: Lines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    line.LastUpdate = DateTime.Now;
                    _context.Update(line);
                    await _context.SaveChangesAsync();
                    //await _rabbit.PublishMessage(JsonConvert.SerializeObject(line), "update.line");
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

        // GET: Lines/Delete/5
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

        // POST: Lines/Delete/5
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
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(line), "delete.line");
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
