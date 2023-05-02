using ContinentalTestAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContinentalTestAPI.Controllers
{
    [Route("api/ContinentalAPI")]
    [ApiController]
    public class ContinentalTestApiController : Controller
    {
        private readonly ContinentalDb _context;

        public ContinentalTestApiController(ContinentalDb context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetComponents")]
        public async Task<IActionResult> GetComponents()
        {
            return Ok(_context.Components.ToList());
        }

        [HttpGet]
        [Route("GetCoordinators")]
        public async Task<IActionResult> GetCoordinators()
        {
            return Ok(_context.Coordinators.ToList());
        }

        [HttpGet]
        [Route("GetDevices")]
        public async Task<IActionResult> GetDevices()
        {
            return Ok(_context.Devices.ToList());
        }

        [HttpGet]
        [Route("GetLines")]
        public async Task<IActionResult> GetLines()
        {
            return Ok(_context.Lines.ToList());
        }

        [HttpGet]
        [Route("GetOperators")]
        public async Task<IActionResult> GetOperators()
        {
            return Ok(_context.Operators.ToList());
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(_context.Products.ToList());
        }

        [HttpGet]
        [Route("GetProductions")]
        public async Task<IActionResult> GetProductions()
        {
            return Ok(_context.Productions.ToList());
        }

        [HttpGet]
        [Route("GetProductionPlans")]
        public async Task<IActionResult> GetProductionsPlans()
        {
            return Ok(_context.Production_Plans.ToList());
        }

        [HttpGet]
        [Route("GetReasons")]
        public async Task<IActionResult> GetReasons()
        {
            return Ok(_context.Reasons.ToList());
        }

        [HttpGet]
        [Route("GetRequests")]
        public async Task<IActionResult> GetRequests()
        {
            return Ok(_context.requests.ToList());
        }

        [HttpGet]
        [Route("GetSchedule_Worker_Lines")]
        public async Task<IActionResult> GetSchedule_Worker_Lines()
        {
            return Ok(_context.Schedule_Worker_Lines.ToList());
        }

        [HttpGet]
        [Route("GetStops")]
        public async Task<IActionResult> GetStops()
        {
            return Ok(_context.Stops.ToList());
        }

        [HttpGet]
        [Route("GetSupervisors")]
        public async Task<IActionResult> GetSupervisors()
        {
            return Ok(_context.Supervisors.ToList());
        }

        [HttpGet]
        [Route("GetWorkers")]
        public async Task<IActionResult> GetWorkers()
        {
            return Ok(_context.Workers.ToList());
        }

    }
}
