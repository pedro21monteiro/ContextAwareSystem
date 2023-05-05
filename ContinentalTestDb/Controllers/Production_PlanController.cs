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
    public class Production_PlanController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public Production_PlanController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Production_Plan
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Production_Plans.Include(p => p.Line).Include(p => p.Product);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Production_Plan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Production_Plans == null)
            {
                return NotFound();
            }

            var production_Plan = await _context.Production_Plans
                .Include(p => p.Line)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production_Plan == null)
            {
                return NotFound();
            }

            return View(production_Plan);
        }

        // GET: Production_Plan/Create
        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Production_Plan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Goal,Name,InitialDate,EndDate,Shift,ProductId,LineId")] Production_Plan production_Plan)
        {
            var l = _context.Lines.SingleOrDefault(l => l.Id == production_Plan.LineId);
            var p = _context.Products.SingleOrDefault(p => p.Id == production_Plan.ProductId);
            if (p != null && l != null)
            {
                production_Plan.Line = l;
                production_Plan.Product = p;

                production_Plan.LastUpdate = DateTime.Now;
                _context.Add(production_Plan);
                await _context.SaveChangesAsync();
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(production_Plan), "create.production_plan");
                

                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", production_Plan.LineId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", production_Plan.ProductId);
            return View(production_Plan);
        }

        // GET: Production_Plan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Production_Plans == null)
            {
                return NotFound();
            }

            var production_Plan = await _context.Production_Plans.FindAsync(id);
            if (production_Plan == null)
            {
                return NotFound();
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", production_Plan.LineId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", production_Plan.ProductId);
            return View(production_Plan);
        }

        // POST: Production_Plan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Goal,Name,InitialDate,EndDate,Shift,ProductId,LineId")] Production_Plan production_Plan)
        {
            if (id != production_Plan.Id)
            {
                return NotFound();
            }

            var l = _context.Lines.SingleOrDefault(l => l.Id == production_Plan.LineId);
            var p = _context.Products.SingleOrDefault(p => p.Id == production_Plan.ProductId);
            if (p != null && l != null)
            {
                try
                {
                    production_Plan.Line = l;
                    production_Plan.Product = p;
                    production_Plan.LastUpdate = DateTime.Now;
                    _context.Update(production_Plan);
                    await _rabbit.PublishMessage(JsonConvert.SerializeObject(production_Plan), "update.production_plan");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Production_PlanExists(production_Plan.Id))
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
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", production_Plan.LineId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", production_Plan.ProductId);
            return View(production_Plan);
        }

        // GET: Production_Plan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Production_Plans == null)
            {
                return NotFound();
            }

            var production_Plan = await _context.Production_Plans
                .Include(p => p.Line)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production_Plan == null)
            {
                return NotFound();
            }

            return View(production_Plan);
        }

        // POST: Production_Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Production_Plans == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Production_Plans'  is null.");
            }
            var production_Plan = await _context.Production_Plans.FindAsync(id);
            if (production_Plan != null)
            {
                _context.Production_Plans.Remove(production_Plan);
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(production_Plan), "delete.production_plan");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Production_PlanExists(int id)
        {
          return _context.Production_Plans.Any(e => e.Id == id);
        }
    }
}
