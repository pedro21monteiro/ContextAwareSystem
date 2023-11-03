using ContinentalTestDb.Data;
using ContinentalTestDb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Models.ContinentalModels;

namespace ContinentalTestDb.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly HttpClient httpClient;
        private static string builderHost = System.Environment.GetEnvironmentVariable("BUILDER") ?? "https://localhost:7284";

        public RequestsController(ContinentalTestDbContext context, HttpClient _httpClient)
        {
            _context = context;
            httpClient = _httpClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Requests.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Date,WorkerId,LineId")] Request request)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == request.WorkerId);
            if (worker == null)
            {
                ModelState.AddModelError("WorkerId", "WorkerId inválido. Insira um WorkerId válido.");
                return View();
            }
            var line = await _context.Lines.FirstOrDefaultAsync(l => l.Id == request.LineId);
            if (line == null)
            {
                ModelState.AddModelError("LineId", "LineId inválido. Insira um LineId válido.");
                return View();
            }
            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                string json = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{builderHost}/api/ContextBuilder/CreateResquest", content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
