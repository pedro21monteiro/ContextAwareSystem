using ContinentalTestDb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContinentalTestDb.Controllers
{
    public class ComponentProductController : Controller
    {
        private readonly ContinentalTestDbContext _context;
        public ComponentProductController(ContinentalTestDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComponentProducts
                .Include(p => p.Product)
                .Include(c => c.Component)
                .ToListAsync());
        }
    }
}
