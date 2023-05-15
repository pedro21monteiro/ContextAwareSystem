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
    public class Schedule_Worker_LineController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public Schedule_Worker_LineController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Schedule_Worker_Line
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Schedule_Worker_Lines.Include(s => s.Line).Include(s => s.Operator).Include(s => s.Supervisor);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Schedule_Worker_Line/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Schedule_Worker_Lines == null)
            {
                return NotFound();
            }

            var schedule_Worker_Line = await _context.Schedule_Worker_Lines
                .Include(s => s.Line)
                .Include(s => s.Operator)
                .Include(s => s.Supervisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule_Worker_Line == null)
            {
                return NotFound();
            }

            return View(schedule_Worker_Line);
        }

        // GET: Schedule_Worker_Line/Create
        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name");
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Id");
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "Id", "Id");

            return View();
        }

        // POST: Schedule_Worker_Line/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day,Shift,LineId,OperatorId,SupervisorId")] Schedule_Worker_Line schedule_Worker_Line)
        {
            var l = _context.Lines.SingleOrDefault(l => l.Id == schedule_Worker_Line.LineId);
            var o = _context.Operators.SingleOrDefault(o => o.Id == schedule_Worker_Line.OperatorId);
            var s = _context.Supervisors.SingleOrDefault(s => s.Id == schedule_Worker_Line.SupervisorId);
            if (l != null)
            {
                schedule_Worker_Line.Line = l;
                if (o != null)
                {
                    schedule_Worker_Line.Operator = o;
                }
                if (s != null)
                {
                    schedule_Worker_Line.Supervisor = s;
                }
                schedule_Worker_Line.LastUpdate = DateTime.Now;
                _context.Add(schedule_Worker_Line);
                await _context.SaveChangesAsync();
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(schedule_Worker_Line), "create.swl");
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", schedule_Worker_Line.LineId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Id", schedule_Worker_Line.OperatorId);
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "Id", "Id", schedule_Worker_Line.SupervisorId);
            return View(schedule_Worker_Line);
        }

        // GET: Schedule_Worker_Line/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Schedule_Worker_Lines == null)
            {
                return NotFound();
            }

            var schedule_Worker_Line = await _context.Schedule_Worker_Lines.FindAsync(id);
            if (schedule_Worker_Line == null)
            {
                return NotFound();
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", schedule_Worker_Line.LineId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Id", schedule_Worker_Line.OperatorId);
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "Id", "Id", schedule_Worker_Line.SupervisorId);
            return View(schedule_Worker_Line);
        }

        // POST: Schedule_Worker_Line/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day,Shift,LineId,OperatorId,SupervisorId")] Schedule_Worker_Line schedule_Worker_Line)
        {
            if (id != schedule_Worker_Line.Id)
            {
                return NotFound();
            }

            var l = _context.Lines.SingleOrDefault(l => l.Id == schedule_Worker_Line.LineId);
            var o = _context.Operators.SingleOrDefault(o => o.Id == schedule_Worker_Line.OperatorId);
            var s = _context.Supervisors.SingleOrDefault(s => s.Id == schedule_Worker_Line.SupervisorId);
            if (l != null)
            {
                try
                {
                    schedule_Worker_Line.Line = l;
                    if (o != null)
                    {
                        schedule_Worker_Line.Operator = o;
                    }
                    if (s != null)
                    {
                        schedule_Worker_Line.Supervisor = s;
                    }
                    schedule_Worker_Line.LastUpdate = DateTime.Now;
                    _context.Update(schedule_Worker_Line);
                    //await _rabbit.PublishMessage(JsonConvert.SerializeObject(schedule_Worker_Line), "update.swl");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Schedule_Worker_LineExists(schedule_Worker_Line.Id))
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
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", schedule_Worker_Line.LineId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Id", schedule_Worker_Line.OperatorId);
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "Id", "Id", schedule_Worker_Line.SupervisorId);
            return View(schedule_Worker_Line);
        }

        // GET: Schedule_Worker_Line/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Schedule_Worker_Lines == null)
            {
                return NotFound();
            }

            var schedule_Worker_Line = await _context.Schedule_Worker_Lines
                .Include(s => s.Line)
                .Include(s => s.Operator)
                .Include(s => s.Supervisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule_Worker_Line == null)
            {
                return NotFound();
            }

            return View(schedule_Worker_Line);
        }

        // POST: Schedule_Worker_Line/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Schedule_Worker_Lines == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Schedule_Worker_Lines'  is null.");
            }
            var schedule_Worker_Line = await _context.Schedule_Worker_Lines.FindAsync(id);
            if (schedule_Worker_Line != null)
            {
                _context.Schedule_Worker_Lines.Remove(schedule_Worker_Line);
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(schedule_Worker_Line), "delete.swl");

            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Schedule_Worker_LineExists(int id)
        {
          return _context.Schedule_Worker_Lines.Any(e => e.Id == id);
        }
    }
}
