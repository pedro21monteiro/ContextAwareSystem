using ContextServer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Context_aware_System.Controllers
{
    [Route("api/ContextService")]
    [ApiController]
    public class ContextService : Controller
    {
        private readonly ContextAwareDb _context;
        //para experimentar os datetimes 2023-11-20T11:11:11Z

        public ContextService(ContextAwareDb context)
        {
            _context = context;
        }

        //Services

        [HttpGet]
        [Route("GetLines")]
        public async Task<IActionResult> GetLines()
        {
            return Ok(_context.Lines);
        }  
    }
}
