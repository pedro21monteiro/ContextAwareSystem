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

        public Schedule_Worker_LineController(ContinentalTestDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Schedule_Worker_Lines.Include(s => s.Line).Include(s => s.Operator).Include(s => s.Supervisor);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name");
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Id");
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "Id", "Id");

            return View();
        }

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
                _context.Add(schedule_Worker_Line);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", schedule_Worker_Line.LineId);
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Id", schedule_Worker_Line.OperatorId);
            ViewData["SupervisorId"] = new SelectList(_context.Supervisors, "Id", "Id", schedule_Worker_Line.SupervisorId);
            return View(schedule_Worker_Line);
        }

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
                    _context.Update(schedule_Worker_Line);
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
