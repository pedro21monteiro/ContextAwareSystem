using ContinentalTestAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetComponents(int? id, string? name, string ?reference, int? category)
        {
            List<Component> listComponents = new List<Component>();
            listComponents = await _context.Components.ToListAsync();
            //o id só pode existir um
            if (id != null)
            {
                listComponents = listComponents.Where(c => c.Id.Equals(id)).ToList();
            }
            if (name != null)
            {
                listComponents = listComponents.Where(c => c.Name.Equals(name)).ToList();
            }
            if (reference != null)
            {
                listComponents = listComponents.Where(c => c.Reference.Equals(reference)).ToList();
            }
            if (category != null)
            {
                listComponents = listComponents.Where(c => c.Category.Equals(category)).ToList();
            }

            return Ok(listComponents);
        }

        [HttpGet]
        [Route("Getcoordinators")]
        public async Task<IActionResult> GetCoordinators(int? id, int? workerId)
        {
            List<Coordinator> listCoordinator = new List<Coordinator>();
            listCoordinator = await _context.Coordinators.ToListAsync();
            //o id só pode existir um
            if (id != null)
            {
                listCoordinator = listCoordinator.Where(c => c.Id.Equals(id)).ToList();
            }
            if (workerId != null)
            {
                listCoordinator = listCoordinator.Where(c => c.WorkerId.Equals(workerId)).ToList();
            }
            return Ok(listCoordinator);
        }

        [HttpGet]
        [Route("GetDevices")]
        public async Task<IActionResult> GetDevices(int? id, int? type, int? lineId)
        {
            List<Device> listDevice = new List<Device>();
            listDevice = await _context.Devices.ToListAsync();
            //o id só pode existir um
            if (id != null)
            {
                listDevice = listDevice.Where(d => d.Id.Equals(id)).ToList();
            }
            if (type != null)
            {
                listDevice = listDevice.Where(d => d.Type.Equals(type)).ToList();
            }
            if (lineId != null)
            {
                listDevice = listDevice.Where(d => d.LineId.Equals(lineId)).ToList();
            }
            return Ok(listDevice);
        }

        [HttpGet]
        [Route("GetLines")]
        public async Task<IActionResult> GetLines(int? id, string? name, bool? priority, int? coordinatorId)
        {
            List<Line> listLines = new List<Line>();
            listLines = await _context.Lines.ToListAsync();

            //o id só pode existir um
            if (id != null)
            {
                listLines = listLines.Where(l => l.Id.Equals(id)).ToList();
            }
            if (name != null)
            {
                listLines = listLines.Where(l => l.Name.Equals(name)).ToList();
            }
            if (priority != null)
            {
                listLines = listLines.Where(l => l.Priority.Equals(priority)).ToList();
            }
            if (coordinatorId != null)
            {
                listLines = listLines.Where(l => l.CoordinatorId.Equals(coordinatorId)).ToList();
            }

            return Ok(listLines);
        }

        [HttpGet]
        [Route("GetOperators")]
        public async Task<IActionResult> GetOperators(int? id, int? workerId)
        {
            List<Operator> listOperators = new List<Operator>();
            listOperators = await _context.Operators.ToListAsync();
            //o id só pode existir um
            if (id != null)
            {
                listOperators = listOperators.Where(o => o.Id.Equals(id)).ToList();
            }
            if (workerId != null)
            {
                listOperators = listOperators.Where(o => o.WorkerId.Equals(workerId)).ToList();
            }

            return Ok(listOperators);
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts(int? id, string? name, string? labelReference, TimeSpan? cycle)
        {
            List<Product> listProducts = new List<Product>();
            listProducts = await _context.Products.ToListAsync();

            if (id != null)
            {
                listProducts = listProducts.Where(p => p.Id.Equals(id)).ToList();
            }
            if (name != null)
            {
                listProducts = listProducts.Where(p => p.Name.Equals(name)).ToList();
            }
            if (labelReference != null)
            {
                listProducts = listProducts.Where(p => p.LabelReference.Equals(labelReference)).ToList();
            }
            if (cycle != null)
            {
                listProducts = listProducts.Where(p => p.Cycle.Equals(cycle)).ToList();
            }

            return Ok(listProducts);
        }

        [HttpGet]
        [Route("GetProductions")]
        public async Task<IActionResult> GetProductions(int? id, int? hour, DateTime? day, int? quantity, int? prodPlanId)
        {
            List<Production> listProduction = new List<Production>();
            listProduction = await _context.Productions.ToListAsync();

            if (id != null)
            {
                listProduction = listProduction.Where(p => p.Id.Equals(id)).ToList();
            }
            if (hour != null)
            {
                listProduction = listProduction.Where(p => p.Hour.Equals(hour)).ToList();
            }
            if (day != null)
            {
                listProduction = listProduction.Where(p => p.Day.Equals(day)).ToList();
            }
            if (quantity != null)
            {
                listProduction = listProduction.Where(p => p.Quantity.Equals(quantity)).ToList();
            }
            if (prodPlanId != null)
            {
                listProduction = listProduction.Where(p => p.Production_PlanId.Equals(prodPlanId)).ToList();
            }

            return Ok(listProduction);
        }

        [HttpGet]
        [Route("GetProdPlans")]
        public async Task<IActionResult> GetProdPlans(int? id, int? goal, string? name, DateTime? inicialDate, DateTime? endDate, int? productId, int? lineId)
        {
            List<Production_Plan> listProdPlans = new List<Production_Plan>();
            listProdPlans = await _context.Production_Plans.ToListAsync();

            if (id != null)
            {
                listProdPlans = listProdPlans.Where(p => p.Id.Equals(id)).ToList();
            }
            if (goal != null)
            {
                listProdPlans = listProdPlans.Where(p => p.Goal.Equals(goal)).ToList();
            }
            if (name != null)
            {
                listProdPlans = listProdPlans.Where(p => p.Name.Equals(name)).ToList();
            }
            if (inicialDate != null)
            {
                listProdPlans = listProdPlans.Where(p => p.InitialDate.Equals(inicialDate)).ToList();
            }
            if (endDate != null)
            {
                listProdPlans = listProdPlans.Where(p => p.EndDate.Equals(endDate)).ToList();
            }
            if (productId != null)
            {
                listProdPlans = listProdPlans.Where(p => p.ProductId.Equals(productId)).ToList();
            }
            if (lineId != null)
            {
                listProdPlans = listProdPlans.Where(p => p.LineId.Equals(lineId)).ToList();
            }

            return Ok(listProdPlans);
        }

        [HttpGet]
        [Route("GetReasons")]
        public async Task<IActionResult> GetReasons(int? id, string? description)
        {
            List<Reason> listReasons = new List<Reason>();
            listReasons = await _context.Reasons.ToListAsync();
            if (id != null)
            {
                listReasons = listReasons.Where(r => r.Id.Equals(id)).ToList();
            }
            if (description != null)
            {
                listReasons = listReasons.Where(r => r.Description.Equals(description)).ToList();
            }

            return Ok(listReasons);
        }

        [HttpGet]
        [Route("GetSchedules")]
        public async Task<IActionResult> GetSchedules(int? id, DateTime? day, int? shift, int? lineId, int? operatorId, int? supervisorId)
        {
            List<Schedule_Worker_Line> listSchedules = new List<Schedule_Worker_Line>();
            listSchedules = await _context.Schedule_Worker_Lines.ToListAsync();

            if (id != null)
            {
                listSchedules = listSchedules.Where(s => s.Id.Equals(id)).ToList();
            }
            if (day != null)
            {
                listSchedules = listSchedules.Where(s => s.Day.Equals(day)).ToList();
            }
            if (shift != null)
            {
                listSchedules = listSchedules.Where(s => s.Shift == shift).ToList();
            }
            if (lineId != null)
            {
                listSchedules = listSchedules.Where(s => s.LineId == lineId).ToList();
            }
            if (operatorId != null)
            {
                listSchedules = listSchedules.Where(s => s.OperatorId == operatorId).ToList();
            }
            if (supervisorId != null)
            {
                listSchedules = listSchedules.Where(s => s.SupervisorId == supervisorId).ToList();
            }

            return Ok(listSchedules);
        }

        [HttpGet]
        [Route("GetStops")]
        public async Task<IActionResult> GetStops(int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId)
        {

            List<Stop> listStops = new List<Stop>();
            listStops = await _context.Stops.ToListAsync();

            if (id != null)
            {
                listStops = listStops.Where(s => s.Id.Equals(id)).ToList();
            }
            if (planned != null)
            {
                listStops = listStops.Where(s => s.Planned == planned).ToList();
            }
            if (initialDate != null)
            {
                listStops = listStops.Where(s => s.InitialDate.Equals(initialDate)).ToList();
            }
            if (endDate != null)
            {
                listStops = listStops.Where(s => s.EndDate.Equals(endDate)).ToList();
            }
            if (duration != null)
            {
                listStops = listStops.Where(s => s.Duration.Equals(duration)).ToList();
            }
            if (shift != null)
            {
                listStops = listStops.Where(s => s.Shift == shift).ToList();
            }
            if (reasonId != null)
            {
                listStops = listStops.Where(s => s.ReasonId.Equals(reasonId)).ToList();
            }
            if (lineId != null)
            {
                listStops = listStops.Where(s => s.LineId.Equals(lineId)).ToList();
            }

            return Ok(listStops);
        }

        [HttpGet]
        [Route("GetSupervisors")]
        public async Task<IActionResult> GetSupervisors(int? Id, int? workerId)
        {
            List<Supervisor> listSupervisor = new List<Supervisor>();
            listSupervisor = await _context.Supervisors.ToListAsync();

            if (Id != null)
            {
                listSupervisor = listSupervisor.Where(s => s.Id.Equals(Id)).ToList();
            }
            if (workerId != null)
            {
                listSupervisor = listSupervisor.Where(s => s.WorkerId.Equals(workerId)).ToList();
            }

            return Ok(listSupervisor);

        }

        [HttpGet]
        [Route("GetWorkers")]
        public async Task<IActionResult> GetWorkers(int? id, string? idFirebase, string? username, string? email, int? role)
        {
            List<Worker> listWorkers = new List<Worker>();
            listWorkers = await _context.Workers.ToListAsync();

            if (id != null)
            {
                listWorkers = listWorkers.Where(w => w.Id.Equals(id)).ToList();
            }
            if (idFirebase != null)
            {
                listWorkers = listWorkers.Where(w => w.IdFirebase.Equals(idFirebase)).ToList();
            }
            if (username != null)
            {
                listWorkers = listWorkers.Where(w => w.UserName == username).ToList();
            }
            if (email != null)
            {
                listWorkers = listWorkers.Where(w => w.Email == email).ToList();
            }
            if (role != null)
            {
                listWorkers = listWorkers.Where(w => w.Role.Equals(role)).ToList();
            }

            return Ok(listWorkers);
        }

        [HttpGet]
        [Route("GetComponentProducts")]
        public async Task<IActionResult> GetComponentProducts(int? id, int? componentId, int? productId, int? quantidade)
        {
            List<ComponentProduct> listComponentProducts = new List<ComponentProduct>();
            listComponentProducts = await _context.ComponentProducts.ToListAsync();

            if (id != null)
            {
                listComponentProducts = listComponentProducts.Where(c => c.Id.Equals(id)).ToList();
            }
            if (componentId != null)
            {
                listComponentProducts = listComponentProducts.Where(c => c.ComponentId.Equals(componentId)).ToList();
            }
            if (productId != null)
            {
                listComponentProducts = listComponentProducts.Where(c => c.ProductId.Equals(productId)).ToList();
            }
            if (quantidade != null)
            {
                listComponentProducts = listComponentProducts.Where(c => c.Quantidade.Equals(quantidade)).ToList();
            }

            return Ok(listComponentProducts);
        }
    }
}
