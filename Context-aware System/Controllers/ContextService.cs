using ContextServer.Data;
using Microsoft.AspNetCore.Mvc;
using Models.ContextModels;

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
        [Route("GetComponents")]
        public async Task<IActionResult> GetComponents(int? Id, string? name, string reference, int? category)
        {   
            List<Component> listComponents = new List<Component>();
            listComponents = _context.Components.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listComponents = listComponents.Where(c => c.Id == Id).ToList();
            }
            if (name != null)
            {
                listComponents = listComponents.Where(c => c.Name == name).ToList();
            }
            if (reference != null)
            {
                listComponents = listComponents.Where(c => c.Reference == reference).ToList();
            }
            if (category != null)
            {
                listComponents = listComponents.Where(c => c.Category == category).ToList();
            }

            return Ok(listComponents);
        }

        [HttpGet]
        [Route("Getcoordinators")]
        public async Task<IActionResult> GetCoordinators(int? Id, int? workerId)
        {
            List<Coordinator> listCoordinator = new List<Coordinator>();
            listCoordinator = _context.Coordinators.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listCoordinator = listCoordinator.Where(c => c.Id == Id).ToList();
            }
            if (workerId != null)
            {
                listCoordinator = listCoordinator.Where(c => c.WorkerId == workerId).ToList();
            }
            return Ok(listCoordinator);
        }

        [HttpGet]
        [Route("GetDevices")]
        public async Task<IActionResult> GetDevices(int? Id, int? Type, int? LineId)
        {
            List<Device> listDevice = new List<Device>();
            listDevice = _context.Devices.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listDevice = listDevice.Where(d => d.Id == Id).ToList();
            }
            if (Type != null)
            {
                listDevice = listDevice.Where(d => d.Type == Type).ToList();
            }
            if (LineId != null)
            {
                listDevice = listDevice.Where(d => d.LineId == LineId).ToList();
            }
            return Ok(listDevice);
        }

        [HttpGet]
        [Route("GetLines")]
        public async Task<IActionResult> GetLines(int? Id, string? name, bool? priority, int? coordinatorId)
        {
            List<Line> listLines = new List<Line>();
            listLines = _context.Lines.ToList();

            //o id só pode existir um
            if(Id != null)
            {
                listLines = listLines.Where(l => l.Id == Id).ToList();
            }
            if(name != null)
            {
                listLines = listLines.Where(l => l.Name == name).ToList();
            }
            if (priority != null)
            {
                listLines = listLines.Where(l => l.Priority == priority).ToList();
            }
            if (coordinatorId != null)
            {
                listLines = listLines.Where(l => l.CoordinatorId == coordinatorId).ToList();
            }

            return Ok(listLines);
        }

        [HttpGet]
        [Route("GetOperators")]
        public async Task<IActionResult> GetOperators(int? Id, int? workerId)
        {
            List<Operator> listOperators = new List<Operator>();
            listOperators = _context.Operators.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listOperators = listOperators.Where(o => o.Id == Id).ToList();
            }
            if (workerId != null)
            {
                listOperators = listOperators.Where(o => o.WorkerId == workerId).ToList();
            }

            return Ok(listOperators);
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts(int? Id, string? name, string? labelReference, TimeSpan? cycle)
        {
            List<Product> listProducts = new List<Product>();
            listProducts = _context.Products.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listProducts = listProducts.Where(p => p.Id == Id).ToList();
            }
            if (name != null)
            {
                listProducts = listProducts.Where(p => p.Name == name).ToList();
            }
            if (labelReference != null)
            {
                listProducts = listProducts.Where(p => p.LabelReference == labelReference).ToList();
            }
            if (cycle != null)
            {
                listProducts = listProducts.Where(p => p.Cycle.Equals(cycle)).ToList();
            }
            return Ok(listProducts);
        }

        [HttpGet]
        [Route("GetProductions")]
        public async Task<IActionResult> GetProductions(int? Id, int? hour, DateTime? day, int? quantity, int? prodPlanId)
        {
            List<Production> listProduction = new List<Production>();
            listProduction = _context.Productions.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listProduction = listProduction.Where(p => p.Id == Id).ToList();
            }
            if (hour != null)
            {
                listProduction = listProduction.Where(p => p.Hour == hour).ToList();
            }
            if (hour != null)
            {
                listProduction = listProduction.Where(p => p.Day.Equals(day)).ToList();
            }
            if (quantity != null)
            {
                listProduction = listProduction.Where(p => p.Quantity == quantity).ToList();
            }
            if (prodPlanId != null)
            {
                listProduction = listProduction.Where(p => p.Production_PlanId == prodPlanId).ToList();
            }
            return Ok(listProduction);
        }

        [HttpGet]
        [Route("GetProdPlans")]
        public async Task<IActionResult> GetProdPlans(int? Id, int? Goal, string? name, DateTime? inicialDate, DateTime? endDate,int? productId,int? lineId)
        {
            List<Production_Plan> listProdPlans = new List<Production_Plan>();
            listProdPlans = _context.Production_Plans.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listProdPlans = listProdPlans.Where(p => p.Id == Id).ToList();
            }
            if (Goal != null)
            {
                listProdPlans = listProdPlans.Where(p => p.Goal == Goal).ToList();
            }
            if (name != null)
            {
                listProdPlans = listProdPlans.Where(p => p.Name == name).ToList();
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
                listProdPlans = listProdPlans.Where(p => p.ProductId == productId).ToList();
            }
            if (lineId != null)
            {
                listProdPlans = listProdPlans.Where(p => p.LineId == lineId).ToList();
            }
            return Ok(listProdPlans);
        }

        [HttpGet]
        [Route("GetReasons")]
        public async Task<IActionResult> GetReasons(int? id, string? description)
        {
            //            public int Id { get; set; }
            //[Required]
            //public string Description { get; set; } = string.Empty;
            List<Reason> listreasons = new List<Reason>();
            listreasons = _context.Reasons.ToList();
            //o id só pode existir um
            if (id != null)
            {
                listreasons = listreasons.Where(r => r.Id == id).ToList();
            }
            if (description != null)
            {
                listreasons = listreasons.Where(r => r.Description == description).ToList();
            }
            return Ok(listreasons);
        }

        [HttpGet]
        [Route("GetRequests")]
        public async Task<IActionResult> GetRequests(int? id, int? type, DateTime? date, int? workerId)
        {
            List<Request> listRequests = new List<Request>();
            listRequests = _context.requests.ToList();
            if (id != null)
            {
                listRequests = listRequests.Where(r => r.Id == id).ToList();
            }
            if (type != null)
            {
                listRequests = listRequests.Where(r => r.Type == type).ToList();
            }
            if (date != null)
            {
                listRequests = listRequests.Where(r => r.Date == date).ToList();
            }
            if (workerId != null)
            {
                listRequests = listRequests.Where(r => r.WorkerId == workerId).ToList();
            }
            return Ok(listRequests);
        }

        [HttpGet]
        [Route("GetSchedules")]
        public async Task<IActionResult> GetSchedules(int? id, DateTime? day, int? shift, int? lineId, int? operatorId, int? supervisorId)
        {
            List<Schedule_Worker_Line> listSchedules = new List<Schedule_Worker_Line>();
            listSchedules = _context.Schedule_Worker_Lines.ToList();
            if (id != null)
            {
                listSchedules = listSchedules.Where(s => s.Id == id).ToList();
            }
            if (!day.Equals(null))
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
        public async Task<IActionResult> GetStops(int? id,bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration,int? shift,int? lineId,int? reasonId)
        {

            List<Stop> listStops = new List<Stop>();
            listStops = _context.Stops.ToList();
            if (id != null)
            {
                listStops = listStops.Where(s => s.Id == id).ToList();
            }
            if (planned != null)
            {
                listStops = listStops.Where(s => s.Planned == planned).ToList();
            }
            if (!initialDate.Equals(null))
            {
                listStops = listStops.Where(s => s.InitialDate.Equals(initialDate)).ToList();
            }
            if (!endDate.Equals(null))
            {
                listStops = listStops.Where(s => s.EndDate.Equals(endDate)).ToList();
            }
            if (!duration.Equals(null))
            {
                listStops = listStops.Where(s => s.Duration.Equals(duration)).ToList();
            }
            if (shift != null)
            {
                listStops = listStops.Where(s => s.Shift == shift).ToList();
            }
            if (reasonId != null)
            {
                listStops = listStops.Where(s => s.ReasonId == reasonId).ToList();
            }
            if (lineId != null)
            {
                listStops = listStops.Where(s => s.LineId == ~lineId).ToList();
            }
            return Ok(listStops);
        }

        [HttpGet]
        [Route("GetSupervisors")]
        public async Task<IActionResult> GetSupervisors(int? Id, int? workerId)
        {
            List<Supervisor> listSupervisor = new List<Supervisor>();
            listSupervisor = _context.Supervisors.ToList();
            //o id só pode existir um
            if (Id != null)
            {
                listSupervisor = listSupervisor.Where(c => c.Id == Id).ToList();
            }
            if (workerId != null)
            {
                listSupervisor = listSupervisor.Where(c => c.WorkerId == workerId).ToList();
            }
            return Ok(listSupervisor);

        }

        [HttpGet]
        [Route("GetWorkers")]
        public async Task<IActionResult> GetWorkers(int? id, string? idFirebase, string? username, string? email, int? role)
        {
            List<Worker> listWorkers = new List<Worker>();
            listWorkers = _context.Workers.ToList();
            //o id só pode existir um
            if (id != null)
            {
                listWorkers = listWorkers.Where(w => w.Id == id).ToList();
            }
            if (idFirebase != null)
            {
                listWorkers = listWorkers.Where(w => w.IdFirebase == idFirebase).ToList();
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
                listWorkers = listWorkers.Where(w => w.Role == role).ToList();
            }
            return Ok(listWorkers);
        }

    }
}
