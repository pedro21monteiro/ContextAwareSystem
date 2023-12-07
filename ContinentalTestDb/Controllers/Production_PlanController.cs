using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContinentalTestDb.Data;
using Models.ContinentalModels;

namespace ContinentalTestDb.Controllers
{
    public class Production_PlanController : Controller
    {
        private readonly ContinentalTestDbContext _context;

        public Production_PlanController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Production_Plans.Include(p => p.Line).Include(p => p.Product);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

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

                _context.Add(production_Plan);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", production_Plan.LineId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", production_Plan.ProductId);
            return View(production_Plan);
        }

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
                    _context.Update(production_Plan);
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
