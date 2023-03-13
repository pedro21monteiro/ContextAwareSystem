using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContinentalTestDb.Data;
using ContinentalTestDb.Models;
using ContinentalTestDb.Services;
using Newtonsoft.Json;

namespace ContinentalTestDb.Controllers
{
    public class DevicesController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public DevicesController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Devices.Include(d => d.Line);
            return View(await continentalTestDbContext.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Line)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,LineId")] Device device)
        {
            var l = _context.Lines.SingleOrDefault(l => l.Id == device.LineId);
            if (l != null)
            {
                device.Line = l;
                _context.Add(device);
                await _context.SaveChangesAsync();
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(device), "create.device");
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", device.LineId);
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", device.LineId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,LineId")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            var l = _context.Lines.SingleOrDefault(l => l.Id == device.LineId);
            if (l != null)
            {
                try
                {
                    device.Line = l;
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                    await _rabbit.PublishMessage(JsonConvert.SerializeObject(device), "update.device");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            ViewData["LineId"] = new SelectList(_context.Lines, "Id", "Name", device.LineId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Line)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Devices == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Devices'  is null.");
            }
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(device), "delete.device");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
          return _context.Devices.Any(e => e.Id == id);
        }
    }
}
