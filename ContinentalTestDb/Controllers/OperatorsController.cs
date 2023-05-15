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
    public class OperatorsController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public OperatorsController(ContinentalTestDbContext context, RabbitMqService rabbit = null)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Operators
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Operators.Include(c => c.Worker);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Operators/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Operators/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName");
            return View();
        }

        // POST: Operators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId")] Operator _operator)
        {
            var o = _context.Workers.SingleOrDefault(s => s.Id == _operator.WorkerId);
            if (o != null)
            {   
                _operator.LastUpdate = DateTime.Now;
                _context.Add(_operator);
                await _context.SaveChangesAsync();
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(_operator), "create.operator");
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", _operator.WorkerId);
            return View(_operator);
        }

        // GET: Operators/Edit/5
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

        // POST: Operators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    @operator.LastUpdate = DateTime.Now;
                    //await _rabbit.PublishMessage(JsonConvert.SerializeObject(@operator), "update.operator");
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

        // GET: Operators/Delete/5
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

        // POST: Operators/Delete/5
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
               // await _rabbit.PublishMessage(JsonConvert.SerializeObject(@operator), "delete.operator");
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
