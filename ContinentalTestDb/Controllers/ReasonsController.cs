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
        private readonly RabbitMqService _rabbit;

        public ReasonsController(ContinentalTestDbContext context, RabbitMqService rabbit = null)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Reasons
        public async Task<IActionResult> Index()
        {
              return View(await _context.Reasons.ToListAsync());
        }

        // GET: Reasons/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Reasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Reason reason)
        {
            if (ModelState.IsValid)
            {
                reason.LastUpdate = DateTime.Now;
                _context.Add(reason);
                await _context.SaveChangesAsync();
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(reason), "create.reason");

                return RedirectToAction(nameof(Index));
            }
            return View(reason);
        }

        // GET: Reasons/Edit/5
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

        // POST: Reasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    reason.LastUpdate = DateTime.Now;
                    _context.Update(reason);
                    await _context.SaveChangesAsync();
                    //await _rabbit.PublishMessage(JsonConvert.SerializeObject(reason), "update.reason");
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

        // GET: Reasons/Delete/5
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

        // POST: Reasons/Delete/5
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
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(reason), "delete.reason");
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
