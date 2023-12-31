﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContinentalTestDb.Data;
using Models.ContinentalModels;


namespace ContinentalTestDb.Controllers
{
    public class OperatorsController : Controller
    {
        private readonly ContinentalTestDbContext _context;

        public OperatorsController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Operators.Include(c => c.Worker);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId")] Operator _operator)
        {
            var o = _context.Workers.SingleOrDefault(s => s.Id == _operator.WorkerId);
            if (o != null)
            {   
                _context.Add(_operator);
                await _context.SaveChangesAsync();
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(_operator), "create.operator");
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", _operator.WorkerId);
            return View(_operator);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Operators == null)
            {
                return NotFound();
            }

            var @operator = await _context.Operators.FindAsync(id);
            if (@operator == null)
            {
                return NotFound();
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", @operator.WorkerId);
            return View(@operator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkerId")] Operator @operator)
        {
            if (id != @operator.Id)
            {
                return NotFound();
            }

            var w = _context.Workers.SingleOrDefault(w => w.Id == @operator.WorkerId);
            if (w != null)
            {
                try
                {
                    _context.Update(@operator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperatorExists(@operator.Id))
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
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", @operator.WorkerId);
            return View(@operator);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Operators == null)
            {
                return NotFound();
            }

            var @operator = await _context.Operators
                .Include(c => c.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@operator == null)
            {
                return NotFound();
            }

            return View(@operator);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Operators == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Operators'  is null.");
            }
            var @operator = await _context.Operators.FindAsync(id);
            if (@operator != null)
            {
                _context.Operators.Remove(@operator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperatorExists(int id)
        {
          return _context.Operators.Any(e => e.Id == id);
        }
    }
}
