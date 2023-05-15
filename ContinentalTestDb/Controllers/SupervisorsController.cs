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
        private readonly RabbitMqService _rabbit;

        public SupervisorsController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Supervisors
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Supervisors.Include(s => s.Worker);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Supervisors/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Supervisors/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName");
            return View();
        }

        // POST: Supervisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId")] Supervisor supervisor)
        {
            var w = _context.Workers.SingleOrDefault(w => w.Id == supervisor.WorkerId);
            if (w != null)
            {
                supervisor.LastUpdate = DateTime.Now;
                _context.Add(supervisor);
                await _context.SaveChangesAsync();
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(supervisor), "create.supervisor");
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", supervisor.WorkerId);
            return View(supervisor);
        }

        // GET: Supervisors/Edit/5
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

        // POST: Supervisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    supervisor.LastUpdate = DateTime.Now;
                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();
                    //await _rabbit.PublishMessage(JsonConvert.SerializeObject(supervisor), "update.supervisor");
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

        // GET: Supervisors/Delete/5
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

        // POST: Supervisors/Delete/5
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
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(supervisor), "delete.supervisor");
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
