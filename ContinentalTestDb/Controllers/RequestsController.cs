using ContinentalTestDb.Data;
using ContinentalTestDb.Models;
using ContinentalTestDb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ContinentalTestDb.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;

        public RequestsController(ContinentalTestDbContext context, RabbitMqService rabbit)
        {
            _context = context;
            _rabbit = rabbit;
        }

        public async Task<IActionResult> Index()
        {
            var continentalTestDbContext = _context.Requests.Include(r => r.Worker);
            return View(await continentalTestDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName");
            return View();
        }

        // POST: Coordinators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Date,WorkerId,LineId,Device")] Request request)
        {
            var r = _context.Workers.SingleOrDefault(w => w.Id == request.WorkerId);
            if (r != null)
            {   
                request.LastUpdate = DateTime.Now;
                _context.Add(request);
                await _context.SaveChangesAsync();
                await _rabbit.PublishMessage(JsonConvert.SerializeObject(request), "create.request");
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", request.WorkerId);
            return View(request);
        }
    }
}
