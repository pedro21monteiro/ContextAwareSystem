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
    public class ProductionsController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public ProductionsController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Productions
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Productions.Include(p => p.Prod_Plan);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productions == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .Include(p => p.Prod_Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // GET: Productions/Create
        public IActionResult Create()
        {
            ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name");
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hour,Day,Quantity,Production_PlanId")] Production production)
        {
            var pp = _context.Production_Plans.SingleOrDefault(p => p.Id == production.Production_PlanId);
            if (pp != null)
            {
                production.Prod_Plan = pp;
                production.LastUpdate = DateTime.Now;
                _context.Add(production);
                await _context.SaveChangesAsync();
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(production), "create.production");

                return RedirectToAction(nameof(Index));
            }
            ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name", production.Production_PlanId);
            return View(production);
        }

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productions == null)
            {
                return NotFound();
            }

            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }
            ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name", production.Production_PlanId);
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Hour,Day,Quantity,Production_PlanId")] Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            var pp = _context.Production_Plans.SingleOrDefault(p => p.Id == production.Production_PlanId);
            if (pp != null)
            {
                try
                {
                    production.Prod_Plan = pp;
                    production.LastUpdate = DateTime.Now;
                    _context.Update(production);
                    await _rabbit.PublishMessage(JsonConvert.SerializeObject(production), "update.production");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Id))
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
            ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name", production.Production_PlanId);
            return View(production);
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productions == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .Include(p => p.Prod_Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productions == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Productions'  is null.");
            }
            var production = await _context.Productions.FindAsync(id);
            if (production != null)
            {
                _context.Productions.Remove(production);
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(production), "delete.production");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionExists(int id)
        {
          return _context.Productions.Any(e => e.Id == id);
        }
    }
}
