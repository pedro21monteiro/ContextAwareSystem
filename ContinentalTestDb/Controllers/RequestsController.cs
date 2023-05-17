using ContinentalTestDb.Data;
using Models.ContinentalModels;
using ContinentalTestDb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace ContinentalTestDb.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly RabbitMqService _rabbit;
        private readonly HttpClient httpClient;

        public RequestsController(ContinentalTestDbContext context, RabbitMqService rabbit, HttpClient _httpClient)
        {
            _context = context;
            _rabbit = rabbit;
            httpClient = _httpClient;
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

                try { 
                    string json = JsonConvert.SerializeObject(request);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7284/api/ContextBuilder/CreateResquest", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        Console.WriteLine("Erro");
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                //await _rabbit.PublishMessage(JsonConvert.SerializeObject(request), "create.request");

                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "UserName", request.WorkerId);
            return View(request);
        }
    }
}
