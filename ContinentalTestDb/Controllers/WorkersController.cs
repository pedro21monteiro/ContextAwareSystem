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
    public class WorkersController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public WorkersController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Workers.ToListAsync());
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdFirebase,UserName,Email,Role")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);
                //criar coordenador ou supervisor relativamente ao role Role (1-coordenador , 2-Operador , 3-Supervisor)
                if(worker.Role == 1)
                {
                    Coordinator coordinator = new Coordinator();
                    coordinator.Worker = worker;
                    coordinator.WorkerId = worker.Id;
                    _context.Add(coordinator);
                }
                if (worker.Role == 2)
                {
                    Operator @operator = new Operator();
                    @operator.Worker = worker;
                    @operator.WorkerId = worker.Id;
                    _context.Add(@operator);
                }
                if (worker.Role == 3)
                {
                    Supervisor supervisor = new Supervisor();
                    supervisor.Worker = worker;
                    supervisor.WorkerId = worker.Id;
                    _context.Add(supervisor);
                }
                await _context.SaveChangesAsync();
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(worker), "create.worker");
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdFirebase,UserName,Email,Role")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);

                    //criar coordenador ou supervisor relativamente ao role Role (1-coordenador , 2-Operador , 3-Supervisor)
                    if (worker.Role == 1)
                    {
                        Coordinator coordinator = new Coordinator();
                        coordinator.Worker = worker;
                        coordinator.WorkerId = worker.Id;
                        _context.Add(coordinator);
                        //remover sup e ope
                        await RemoveSupervisor(worker.Id);
                        await RemoveOperator(worker.Id);
                    }
                    if (worker.Role == 2)
                    {
                        Operator @operator = new Operator();
                        @operator.Worker = worker;
                        @operator.WorkerId = worker.Id;
                        _context.Add(@operator);
                        //remover sup e coord
                        await RemoveSupervisor(worker.Id);
                        await RemoveCoordinator(worker.Id);
                    }
                    if (worker.Role == 3)
                    {
                        Supervisor supervisor = new Supervisor();
                        supervisor.Worker = worker;
                        supervisor.WorkerId = worker.Id;
                        _context.Add(supervisor);
                        //remover ope e coord
                        await RemoveOperator(worker.Id);
                        await RemoveCoordinator(worker.Id);
                    }
                    //remover outro que exista


                    await _context.SaveChangesAsync();
                    //await _rabbit.PublishMessage(JsonConvert.SerializeObject(worker), "update.worker");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workers == null)
            {
                return Problem("Entity set 'ContinentalTestDbContext.Workers'  is null.");
            }
            var worker = await _context.Workers.FindAsync(id);
            if (worker != null)
            {
                _context.Workers.Remove(worker);
                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(worker), "delete.worker");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id)
        {
          return _context.Workers.Any(e => e.Id == id);
        }

        public async Task RemoveOperator(int workerId)
        {
            var ope = _context.Operators.First(o=>o.WorkerId == workerId);
            if(ope != null)
            {
               _context.Remove(ope);
               await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveSupervisor(int workerId)
        {
            var sup = _context.Supervisors.First(s => s.WorkerId == workerId);
            if (sup != null)
            {
                _context.Remove(sup);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCoordinator(int workerId)
        {
            var coordinator = _context.Coordinators.First(c => c.WorkerId == workerId);
            if (coordinator != null)
            {
                _context.Remove(coordinator);
                await _context.SaveChangesAsync();
            }
        }


    }
}
