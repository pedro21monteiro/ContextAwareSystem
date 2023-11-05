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

        public ProductionsController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Productions.Include(p => p.Prod_Plan);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hour,Day,Quantity,Production_PlanId")] Production production)
        {
            var pp = _context.Production_Plans.SingleOrDefault(p => p.Id == production.Production_PlanId);
            if (pp == null)
            {
                ModelState.AddModelError("Production_PlanId", "Production_PlanId inválido. Insira um Production_PlanId válido.");
                ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name", production.Production_PlanId);
                return View(production);
            }
            if(production.Hour < 0 || production.Hour >= 24)
            {
                ModelState.AddModelError("Hour", "Hour inválido. Insira uma hora válida (0-23).");
                ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name", production.Production_PlanId);
                return View(production);
            }
            production.Prod_Plan = pp;
            _context.Add(production);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));            
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Hour,Day,Quantity,Production_PlanId")] Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }
            var pp = _context.Production_Plans.SingleOrDefault(p => p.Id == production.Production_PlanId);
            if (pp == null)
            {
                ModelState.AddModelError("Production_PlanId", "Production_PlanId inválido. Insira um Production_PlanId válido.");
                ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name", production.Production_PlanId);
                return View(production);
            }
            if (production.Hour < 0 || production.Hour >= 24)
            {
                ModelState.AddModelError("Hour", "Hour inválido. Insira uma hora válida (0-23).");
                ViewData["Production_PlanId"] = new SelectList(_context.Production_Plans, "Id", "Name", production.Production_PlanId);
                return View(production);
            }
            try
            {
                production.Prod_Plan = pp;
                _context.Update(production);
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
