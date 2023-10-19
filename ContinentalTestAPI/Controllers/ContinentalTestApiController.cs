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
        public async Task<IActionResult> GetComponents(DateTime? InicialDate)
        {
            List<Component> components = new List<Component>();

            if (InicialDate != null)
            {
                foreach (var c in _context.Components)
                {
                    if (c.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        components.Add(c);
                    }
                }
                return Ok(components);
            }
            else
            {
                return Ok(_context.Components.ToList());
            }

        }

        [HttpGet]
        [Route("GetCoordinators")]
        public async Task<IActionResult> GetCoordinators(DateTime? InicialDate)
        {
            List<Coordinator> coordinators = new List<Coordinator>();

            if (InicialDate != null)
            {
                foreach (var c in _context.Coordinators.Include(c => c.Worker))
                {
                    if (c.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        coordinators.Add(c);
                    }
                }
                return Ok(coordinators);
            }
            else
            {
                return Ok(_context.Coordinators.ToList());
            }
        }

        [HttpGet]
        [Route("GetDevices")]
        public async Task<IActionResult> GetDevices(DateTime? InicialDate)
        {
            List<Device> devices= new List<Device>();

            if (InicialDate != null)
            {
                foreach (var d in _context.Devices.Include(d=>d.Line))
                {
                    if (d.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        devices.Add(d);
                    }
                }
                return Ok(devices);
            }
            else
            {
                return Ok(_context.Devices.ToList());
            }
        }

        [HttpGet]
        [Route("GetLines")]
        public async Task<IActionResult> GetLines(DateTime? InicialDate)
        {
            List<Line> lines = new List<Line>();

            if (InicialDate != null)
            {
                foreach (var l in _context.Lines.Include(c=>c.Coordinator))
                {
                    if (l.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        lines.Add(l);
                    }
                }
                return Ok(lines);
            }
            else
            {
                return Ok(_context.Lines.ToList());
            }
        }

        [HttpGet]
        [Route("GetOperators")]
        public async Task<IActionResult> GetOperators(DateTime? InicialDate)
        {
            List<Operator> operators = new List<Operator>();

            if (InicialDate != null)
            {
                foreach (var o in _context.Operators.Include(c=>c.Worker))
                {
                    if (o.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        operators.Add(o);
                    }
                }
                return Ok(operators);
            }
            else
            {
                return Ok(_context.Operators.ToList());
            }
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts(DateTime? InicialDate)
        {
            List<Product> products = new List<Product>();

            if (InicialDate != null)
            {
                foreach (var p in _context.Products.ToList())
                {
                    if (p.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        products.Add(p);
                    }
                }
                return Ok(products);
            }
            else
            {
                return Ok(_context.Products.ToList());
            }
        }

        [HttpGet]
        [Route("GetProductions")]
        public async Task<IActionResult> GetProductions(DateTime? InicialDate)
        {
            List<Production> productions = new List<Production>();

            if (InicialDate != null)
            {
                foreach (var p in _context.Productions.Include(p=>p.Prod_Plan))
                {
                    if (p.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        productions.Add(p);
                    }
                }
                return Ok(productions);
            }
            else
            {
                return Ok(_context.Productions.ToList());
            }
        }

        [HttpGet]
        [Route("GetProductionPlans")]
        public async Task<IActionResult> GetProductionsPlans(DateTime? InicialDate)
        {
            List<Production_Plan> production_Plans = new List<Production_Plan>();

            if (InicialDate != null)
            {
                foreach (var p in _context.Production_Plans.Include(p=>p.Line).Include(p=>p.Product))
                {
                    if (p.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        production_Plans.Add(p);
                    }
                }
                return Ok(production_Plans);
            }
            else
            {
                return Ok(_context.Production_Plans.ToList());
            }
        }

        [HttpGet]
        [Route("GetReasons")]
        public async Task<IActionResult> GetReasons(DateTime? InicialDate)
        {
            List<Reason> reasons = new List<Reason>();

            if (InicialDate != null)
            {
                foreach (var r in _context.Reasons)
                {
                    if (r.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        reasons.Add(r);
                    }
                }
                return Ok(reasons);
            }
            else
            {
                return Ok(_context.Reasons.ToList());
            }
        }

        //[HttpGet]
        //[Route("GetRequests")]
        //public async Task<IActionResult> GetRequests(DateTime? InicialDate)
        //{
        //    List<Request> requests = new List<Request>();

        //    if (InicialDate != null)
        //    {
        //        foreach (var r in _context.requests)
        //        {
        //            if (r.LastUpdate.CompareTo(InicialDate) > 0)
        //            {
        //                requests.Add(r);
        //            }
        //        }
        //        return Ok(requests);
        //    }
        //    else
        //    {
        //        return Ok(_context.requests.ToList());
        //    }
        //}

        [HttpGet]
        [Route("GetSchedule_Worker_Lines")]
        public async Task<IActionResult> GetSchedule_Worker_Lines(DateTime? InicialDate)
        {
            List<Schedule_Worker_Line> schedule_Worker_Lines = new List<Schedule_Worker_Line>();

            if (InicialDate != null)
            {
                foreach (var s in _context.Schedule_Worker_Lines.Include(s=>s.Line).Include(s=>s.Supervisor).Include(s => s.Operator))
                {
                    if (s.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        schedule_Worker_Lines.Add(s);
                    }
                }
                return Ok(schedule_Worker_Lines);
            }
            else
            {
                return Ok(_context.Schedule_Worker_Lines.ToList());
            }
        }

        [HttpGet]
        [Route("GetStops")]
        public async Task<IActionResult> GetStops(DateTime? InicialDate)
        {
            List<Stop> stops = new List<Stop>();

            if (InicialDate != null)
            {
                foreach (var s in _context.Stops.Include(s=>s.Reason))
                {
                    if (s.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        stops.Add(s);
                    }
                }
                return Ok(stops);
            }
            else
            {
                return Ok(_context.Stops.ToList());
            }
        }

        [HttpGet]
        [Route("GetSupervisors")]
        public async Task<IActionResult> GetSupervisors(DateTime? InicialDate)
        {
            List<Supervisor> supervisors = new List<Supervisor>();

            if (InicialDate != null)
            {
                foreach (var s in _context.Supervisors.Include(s=>s.Worker))
                {
                    if (s.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        supervisors.Add(s);
                    }
                }
                return Ok(supervisors);
            }
            else
            {
                return Ok(_context.Supervisors.ToList());
            }
        }

        [HttpGet]
        [Route("GetWorkers")]
        public async Task<IActionResult> GetWorkers(DateTime? InicialDate)
        {
            List<Worker> workers = new List<Worker>();

            if (InicialDate != null)
            {
                foreach (var w in _context.Workers)
                {
                    if (w.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        workers.Add(w);
                    }
                }
                return Ok(workers);
            }
            else
            {
                return Ok(_context.Workers.ToList());
            }
        }


        [HttpGet]
        [Route("GetComponentProducts")]
        public async Task<IActionResult> GetComponentProducts(DateTime? InicialDate)
        {
            List<ComponentProduct> componentProducts = new List<ComponentProduct>();

            if (InicialDate != null)
            {
                foreach (var p in _context.ComponentProducts.ToList())
                {
                    if (p.LastUpdate.CompareTo(InicialDate) > 0)
                    {
                        componentProducts.Add(p);
                    }
                }
                return Ok(componentProducts);
            }
            else
            {
                return Ok(_context.ComponentProducts.ToList());
            }
        }

    }
}
