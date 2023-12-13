using ContinentalTestAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.cdc_Models;
using Models.ContinentalModels;

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
        public async Task<IActionResult> GetComponents(int? id,string? name,string? reference,int? category)
        {
            IQueryable<Component> query = _context.Components.AsQueryable();

            if (id != null)
            {
                query = query.Where(c => c.Id == id);
            }
            if (name != null)
            {
                query = query.Where(c => c.Name == name);
            }
            if (reference != null)
            {
                query = query.Where(c => c.Reference == reference);
            }
            if (category != null)
            {
                query = query.Where(c => c.Category == category);
            }

            var listComponents = await query.ToListAsync();

            return Ok(listComponents);
        }

        [HttpGet]
        [Route("Getcoordinators")]
        public async Task<IActionResult> GetCoordinators(int? id, int? workerId)
        {
            IQueryable<Coordinator> query = _context.Coordinators.AsQueryable();

            if (id != null)
            {
                query = query.Where(c => c.Id == id);
            }
            if (workerId != null)
            {
                query = query.Where(c => c.WorkerId == workerId);
            }

            var listCoordinator = await query.ToListAsync();

            return Ok(listCoordinator);
        }

        [HttpGet]
        [Route("GetDevices")]
        public async Task<IActionResult> GetDevices(int? id, int? type, int? lineId)
        {
            IQueryable<Device> query = _context.Devices.AsQueryable();

            if (id != null)
            {
                query = query.Where(d => d.Id == id);
            }
            if (type != null)
            {
                query = query.Where(d => d.Type == type);
            }
            if (lineId != null)
            {
                query = query.Where(d => d.LineId == lineId);
            }

            var listDevice = await query.ToListAsync();

            return Ok(listDevice);
        }

        [HttpGet]
        [Route("GetLines")]
        public async Task<IActionResult> GetLines(int? id, string? name, bool? priority, int? coordinatorId)
        {
            IQueryable<Line> query = _context.Lines.AsQueryable();

            if (id != null)
            {
                query = query.Where(l => l.Id == id);
            }
            if (name != null)
            {
                query = query.Where(l => l.Name.Equals(name));
            }
            if (priority != null)
            {
                query = query.Where(l => l.Priority == priority);
            }
            if (coordinatorId != null)
            {
                query = query.Where(l => l.CoordinatorId == coordinatorId);
            }

            var listLines = await query.ToListAsync();

            return Ok(listLines);
        }

        [HttpGet]
        [Route("GetOperators")]
        public async Task<IActionResult> GetOperators(int? id, int? workerId)
        {
            IQueryable<Operator> query = _context.Operators.AsQueryable();

            if (id != null)
            {
                query = query.Where(o => o.Id == id);
            }
            if (workerId != null)
            {
                query = query.Where(o => o.WorkerId == workerId);
            }

            var listOperators = await query.ToListAsync();

            return Ok(listOperators);
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts(int? id, string? name, string? labelReference, TimeSpan? cycle)
        {
            IQueryable<Product> query = _context.Products.AsQueryable();

            if (id != null)
            {
                query = query.Where(p => p.Id == id);
            }
            if (name != null)
            {
                query = query.Where(p => p.Name.Equals(name));
            }
            if (labelReference != null)
            {
                query = query.Where(p => p.LabelReference.Equals(labelReference));
            }
            if (cycle != null)
            {
                query = query.Where(p => p.Cycle == cycle);
            }

            var listProducts = await query.ToListAsync();

            return Ok(listProducts);
        }

        [HttpGet]
        [Route("GetProductions")]
        public async Task<IActionResult> GetProductions(int? id, int? hour, DateTime? day, int? quantity, int? prodPlanId)
        {
            IQueryable<Production> query = _context.Productions.AsQueryable();

            if (id != null)
            {
                query = query.Where(p => p.Id == id);
            }
            if (hour != null)
            {
                query = query.Where(p => p.Hour == hour);
            }
            if (day != null)
            {
                query = query.Where(p => p.Day.Date == day.Value.Date);
            }
            if (quantity != null)
            {
                query = query.Where(p => p.Quantity == quantity);
            }
            if (prodPlanId != null)
            {
                query = query.Where(p => p.Production_PlanId == prodPlanId);
            }

            var listProduction = await query.ToListAsync();

            return Ok(listProduction);
        }

        [HttpGet]
        [Route("GetProdPlans")]
        public async Task<IActionResult> GetProdPlans(int? id, int? goal, string? name, DateTime? inicialDate, DateTime? endDate, int? productId, int? lineId)
        {
            IQueryable<Production_Plan> query = _context.Production_Plans.AsQueryable();

            if (id != null)
            {
                query = query.Where(p => p.Id == id);
            }
            if (goal != null)
            {
                query = query.Where(p => p.Goal == goal);
            }
            if (name != null)
            {
                query = query.Where(p => p.Name.Equals(name));
            }
            if (inicialDate != null)
            {
                query = query.Where(p => p.InitialDate == inicialDate);
            }
            if (endDate != null)
            {
                query = query.Where(p => p.EndDate == endDate);
            }
            if (productId != null)
            {
                query = query.Where(p => p.ProductId == productId);
            }
            if (lineId != null)
            {
                query = query.Where(p => p.LineId == lineId);
            }

            var listProdPlans = await query.ToListAsync();

            return Ok(listProdPlans);
        }

        [HttpGet]
        [Route("GetReasons")]
        public async Task<IActionResult> GetReasons(int? id, string? description)
        {
            IQueryable<Reason> query = _context.Reasons.AsQueryable();

            if (id != null)
            {
                query = query.Where(r => r.Id == id);
            }
            if (description != null)
            {
                query = query.Where(r => r.Description.Equals(description));
            }

            var listReasons = await query.ToListAsync();

            return Ok(listReasons);
        }

        [HttpGet]
        [Route("GetSchedules")]
        public async Task<IActionResult> GetSchedules(int? id, DateTime? day, int? shift, int? lineId, int? operatorId, int? supervisorId)
        {
            IQueryable<Schedule_Worker_Line> query = _context.Schedule_Worker_Lines.AsQueryable();

            if (id != null)
            {
                query = query.Where(s => s.Id == id);
            }
            if (day != null)
            {
                query = query.Where(s => s.Day.Date == day.Value.Date);
            }
            if (shift != null)
            {
                query = query.Where(s => s.Shift == shift);
            }
            if (lineId != null)
            {
                query = query.Where(s => s.LineId == lineId);
            }
            if (operatorId != null)
            {
                query = query.Where(s => s.OperatorId == operatorId);
            }
            if (supervisorId != null)
            {
                query = query.Where(s => s.SupervisorId == supervisorId);
            }

            var listSchedules = await query.ToListAsync();

            return Ok(listSchedules);
        }

        [HttpGet]
        [Route("GetStops")]
        public async Task<IActionResult> GetStops(int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId)
        {

            IQueryable<Stop> query = _context.Stops.AsQueryable();

            if (id != null)
            {
                query = query.Where(s => s.Id == id);
            }
            if (planned != null)
            {
                query = query.Where(s => s.Planned == planned);
            }
            if (initialDate != null)
            {
                query = query.Where(s => s.InitialDate == initialDate);
            }
            if (endDate != null)
            {
                query = query.Where(s => s.EndDate == endDate);
            }
            if (duration != null)
            {
                query = query.Where(s => s.Duration == duration);
            }
            if (shift != null)
            {
                query = query.Where(s => s.Shift == shift);
            }
            if (reasonId != null)
            {
                query = query.Where(s => s.ReasonId == reasonId);
            }
            if (lineId != null)
            {
                query = query.Where(s => s.LineId == lineId);
            }

            var listStops = await query.ToListAsync();

            return Ok(listStops);
        }

        [HttpGet]
        [Route("GetSupervisors")]
        public async Task<IActionResult> GetSupervisors(int? id, int? workerId)
        {
            IQueryable<Supervisor> query = _context.Supervisors.AsQueryable();

            if (id != null)
            {
                query = query.Where(s => s.Id == id);
            }
            if (workerId != null)
            {
                query = query.Where(s => s.WorkerId == workerId);
            }

            var listSupervisors = await query.ToListAsync();

            return Ok(listSupervisors);

        }

        [HttpGet]
        [Route("GetWorkers")]
        public async Task<IActionResult> GetWorkers(int? id, string? idFirebase, string? username, string? email, int? role)
        {
            IQueryable<Worker> query = _context.Workers.AsQueryable();

            if (id != null)
            {
                query = query.Where(w => w.Id == id);
            }
            if (idFirebase != null)
            {
                query = query.Where(w => w.IdFirebase == idFirebase);
            }
            if (username != null)
            {
                query = query.Where(w => w.UserName == username);
            }
            if (email != null)
            {
                query = query.Where(w => w.Email == email);
            }
            if (role != null)
            {
                query = query.Where(w => w.Role == role);
            }

            var listWorkers = await query.ToListAsync();

            return Ok(listWorkers);
        }

        [HttpGet]
        [Route("GetComponentProducts")]
        public async Task<IActionResult> GetComponentProducts(int? id, int? componentId, int? productId, int? quantidade)
        {
            IQueryable<ComponentProduct> query = _context.ComponentProducts.AsQueryable();

            if (id != null)
            {
                query = query.Where(c => c.Id == id);
            }
            if (componentId != null)
            {
                query = query.Where(c => c.ComponentId == componentId);
            }
            if (productId != null)
            {
                query = query.Where(c => c.ProductId == productId);
            }
            if (quantidade != null)
            {
                query = query.Where(c => c.Quantidade == quantidade);
            }

            var listComponentProducts = await query.ToListAsync();

            return Ok(listComponentProducts);
        }


        //Implementar os cds, serão pedidos os dados de cdc depois de certo datetime
        [HttpGet]
        [Route("GetCdcStops")]
        public async Task<IActionResult> GetCdcStops(DateTime? InicialDate)
        {
            if (InicialDate != null)
            {
                var cdcStops = await _context.cdc_Stops
                    .Where(s => s.ModificationDate > InicialDate)
                    .ToListAsync();
                return Ok(cdcStops);
            }
            else
            {
                var allCdcStops = await _context.cdc_Stops.ToListAsync();
                return Ok(allCdcStops);
            }
        }

        [HttpGet]
        [Route("GetCdcProductions")]
        public async Task<IActionResult> GetCdcProductions(DateTime? InicialDate)
        {
            if (InicialDate != null)
            {
                var cdcProductions = await _context.cdc_Productions
                    .Where(s => s.ModificationDate > InicialDate)
                    .ToListAsync();
                return Ok(cdcProductions);
            }
            else
            {
                var allCdcProductions = await _context.cdc_Productions.ToListAsync();
                return Ok(allCdcProductions);
            }
        }
    }
}
