using ContinentalTestDb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.FunctionModels;
using Newtonsoft.Json;
using System.Text;

namespace ContinentalTestDb.Controllers
{
    public class MissingComponentsController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        private readonly HttpClient httpClient;
        private static string builderHost = System.Environment.GetEnvironmentVariable("BUILDER") ?? "https://localhost:7284";

        public MissingComponentsController(ContinentalTestDbContext context, HttpClient _httpClient)
        {
            _context = context;
            httpClient = _httpClient;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.MissingComponents.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LineId,ComponentId")] MissingComponent missingComponent)
        {
            var line = await _context.Lines.FirstOrDefaultAsync(l => l.Id == missingComponent.LineId);
            if (line == null)
            {
                ModelState.AddModelError("LineId", "LineId inválido. Insira um LineId válido.");
                return View();
            }
            var component = await _context.Components.FirstOrDefaultAsync(c => c.Id == missingComponent.ComponentId);
            if (component == null)
            {
                ModelState.AddModelError("ComponentId", "ComponentId inválido. Insira um ComponentId válido.");
                return View();
            }
            try
            {
                missingComponent.OrderDate = DateTime.Now;
                _context.Add(missingComponent);
                await _context.SaveChangesAsync();
                string json = JsonConvert.SerializeObject(missingComponent);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{builderHost}/api/ContextBuilder/AddMissingComponent", content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        //delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MissingComponents == null)
            {
                return NotFound();
            }
            var missingComponent = await _context.MissingComponents.FirstOrDefaultAsync(m => m.Id == id);
            if (missingComponent == null)
            {
                return NotFound();
            }
            _context.MissingComponents.Remove(missingComponent);
            await _context.SaveChangesAsync();
            try
            {
                string json = JsonConvert.SerializeObject(missingComponent);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{builderHost}/api/ContextBuilder/RemoveMissingComponent", content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.MissingComponents == null)
        //    {
        //        return Problem("Entity set 'ContinentalTestDbContext.MissingComponents'  is null.");
        //    }
        //    var missingComponent = await _context.MissingComponents.FindAsync(id);
        //    if (missingComponent != null)
        //    {
        //        _context.MissingComponents.Remove(missingComponent);
        //        //await _rabbit.PublishMessage(JsonConvert.SerializeObject(device), "delete.device");
        //    }
        //    await _context.SaveChangesAsync();
        //    try
        //    {
        //        string json = JsonConvert.SerializeObject(missingComponent);
        //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await httpClient.PostAsync($"{builderHost}/api/ContextBuilder/RemoveMissingComponent", content);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
