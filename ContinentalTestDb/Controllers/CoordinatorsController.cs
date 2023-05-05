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
        private readonly RabbitMqService _rabbit;

        public CoordinatorsController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Coordinators
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Coordinators.Include(c => c.Worker);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Coordinators/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Coordinators/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName");
            return View();
        }

        // POST: Coordinators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId")] Coordinator coordinator)
        {
            var w = _context.Workers.SingleOrDefault(w => w.Id == coordinator.WorkerId);
            if (w != null)
            {
                coordinator.LastUpdate = DateTime.Now;
                _context.Add(coordinator);
                await _context.SaveChangesAsync();
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(coordinator), "create.coordinator");
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", coordinator.WorkerId);
            return View(coordinator);
        }

        // GET: Coordinators/Edit/5
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

        // POST: Coordinators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    coordinator.LastUpdate = DateTime.Now;
                    _context.Update(coordinator);
                    await _context.SaveChangesAsync();
                    await _rabbit.PublishMessage(JsonConvert.SerializeObject(coordinator), "update.coordinator");
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

        // GET: Coordinators/Delete/5
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

        // POST: Coordinators/Delete/5
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
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(coordinator), "delete.coordinator");
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
