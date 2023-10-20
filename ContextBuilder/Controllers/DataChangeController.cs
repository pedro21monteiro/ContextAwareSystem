using ContextBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Models.ContextModels;
using Models.JsonModels;

namespace ContextBuilder.Controllers
{
    [Route("api/DataChange")]
    [ApiController]
    public class DataChangeController : Controller
    {
        private readonly ContextBuilderDb _context;


        public DataChangeController(ContextBuilderDb context)
        {
            _context = context;
        }

        //-----------Funçoes CRUD da Base de dados da continental
        // o int operation tem a ver com a função que é para fazer, 1-create, 2-update, 3-delete para já ainda não recebe a dizer o que é para fazer
        // por isso tem de verificar

        //---------------Component
        [HttpPost]
        [Route("ChangeComponent")]
        public async Task<ActionResult> ChangeComponent([FromBody] JsonComponent component)
        {
            var cExistInContext = _context.Components.SingleOrDefault(c => c.Id == component.Id);
            if (cExistInContext == null)
            {
                //Fazer Create
                try
                {
                    Component c = new Component();
                    c.Id = component.Id;
                    c.Name = component.Name;
                    c.Reference = component.Reference;
                    c.Category = component.Category;
                    c.LastUpdate = component.LastUpdate;
                    _context.Add(c);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    cExistInContext.Name = component.Name;
                    cExistInContext.Reference = component.Reference;
                    cExistInContext.Category = component.Category;
                    cExistInContext.LastUpdate = component.LastUpdate;

                    _context.Update(cExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }

        //---------------Coordinator
        [HttpPost]
        [Route("ChangeCoordinator")]
        public async Task<ActionResult> ChangeCoordinator([FromBody] JsonCoordinator coordinator, int? operation)
        {
            var cExistInContext = _context.Coordinators.SingleOrDefault(c => c.Id == coordinator.Id);
            if (cExistInContext == null)
            {
                try
                {
                    Coordinator c = new Coordinator();
                    c.Id = coordinator.Id;
                    c.WorkerId = coordinator.WorkerId;
                    c.LastUpdate = coordinator.LastUpdate;
                    _context.Add(c);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    cExistInContext.WorkerId = coordinator.WorkerId;
                    cExistInContext.LastUpdate = coordinator.LastUpdate;
                    _context.Update(cExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }

            return Ok();
        }

        //---------------Device
        [HttpPost]
        [Route("ChangeDevice")]
        public async Task<ActionResult> ChangeDevice([FromBody] JsonDevice device, int? operation)
        {
            var dExistInContext = _context.Devices.SingleOrDefault(d => d.Id == device.Id);
            if (dExistInContext == null)
            {
                //Create
                try
                {
                    Device d = new Device();
                    d.Id = device.Id;
                    d.Type = device.Type;
                    d.LineId = device.LineId;
                    d.LastUpdate = device.LastUpdate;
                    _context.Add(d);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    dExistInContext.LineId = device.LineId;
                    dExistInContext.LastUpdate = device.LastUpdate;
                    dExistInContext.Type = device.Type;
                    _context.Update(dExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }

        //---------------Line
        [HttpPost]
        [Route("ChangeLine")]
        public async Task<ActionResult> ChangeLine([FromBody] JsonLine line, int? operation)
        {

            var lExistInContext = _context.Lines.SingleOrDefault(l => l.Id == line.Id);
            if (lExistInContext == null)
            {
                try
                {
                    Line l = new Line();
                    l.Id = line.Id;
                    l.Name = line.Name;
                    l.Priority = line.Priority;
                    l.LastUpdate = line.LastUpdate;
                    l.CoordinatorId = line.CoordinatorId;

                    _context.Add(l);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    lExistInContext.CoordinatorId = line.CoordinatorId;
                    lExistInContext.LastUpdate = line.LastUpdate;
                    lExistInContext.Name = line.Name;
                    lExistInContext.Priority = line.Priority;
                    _context.Update(lExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }


        //---------------Operator
        [HttpPost]
        [Route("ChangeOperator")]
        public async Task<ActionResult> ChangeOperator([FromBody] JsonOperator ope, int? operation)
        {
            var oExistInContext = _context.Operators.SingleOrDefault(o => o.Id == ope.Id);
            if (oExistInContext == null)
            {
                try
                {
                    JsonOperator jsonOperator = new JsonOperator();
                    jsonOperator.Id = ope.Id;
                    jsonOperator.WorkerId = ope.WorkerId;
                    jsonOperator.LastUpdate = ope.LastUpdate;

                    Operator o = new Operator();
                    o.Id = ope.Id;
                    o.WorkerId = ope.WorkerId;
                    o.LastUpdate = ope.LastUpdate;

                    _context.Add(o);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    oExistInContext.WorkerId = ope.WorkerId;
                    oExistInContext.LastUpdate = ope.LastUpdate;
                    _context.Update(oExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }

        //---------------Product
        [HttpPost]
        [Route("ChangeProduct")]
        public async Task<ActionResult> ChangeProduct([FromBody] JsonProduct product, int? operation)
        {
            var pExistInContext = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if (pExistInContext == null)
            {
                //Fazer Create
                try
                {
                    Product p = new Product();
                    p.Id = product.Id;
                    p.Name = product.Name;
                    p.LabelReference = product.LabelReference;
                    p.Cycle = product.Cycle;
                    p.LastUpdate = product.LastUpdate;

                    _context.Add(p);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    pExistInContext.Name = product.Name;
                    pExistInContext.LabelReference = product.LabelReference;
                    pExistInContext.Cycle = product.Cycle;
                    pExistInContext.LastUpdate = product.LastUpdate;

                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }

        //---------------Production
        [HttpPost]
        [Route("ChangeProduction")]
        public async Task<ActionResult> ChangeProduction([FromBody] JsonProduction production, int? operation)
        {
            var pExistInContext = _context.Productions.SingleOrDefault(p => p.Id == production.Id);
            if (pExistInContext == null)
            {
                try
                {
                    Production p = new Production();
                    p.Id = production.Id;
                    p.Hour = production.Hour;
                    p.Day = production.Day;
                    p.Quantity = production.Quantity;
                    p.LastUpdate = production.LastUpdate;
                    p.Production_PlanId = production.Production_PlanId;

                    _context.Add(p);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    pExistInContext.Production_PlanId = production.Production_PlanId;
                    pExistInContext.Hour = production.Hour;
                    pExistInContext.Day = production.Day;
                    pExistInContext.Quantity = production.Quantity;
                    pExistInContext.LastUpdate = production.LastUpdate;
                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }


        //---------------ProductionPlan
        [HttpPost]
        [Route("ChangeProductionPlan")]
        public async Task<ActionResult> ChangeProductionPlan([FromBody] JsonProductionPlan production_Plan, int? operation)
        {
            var pExistInContext = _context.Production_Plans.SingleOrDefault(p => p.Id == production_Plan.Id);
            if (pExistInContext == null)
            {
                try
                {
                    Production_Plan p = new Production_Plan();
                    p.Id = production_Plan.Id;
                    p.Goal = production_Plan.Goal;
                    p.Name = production_Plan.Name;
                    p.InitialDate = production_Plan.InitialDate;
                    p.EndDate = production_Plan.EndDate;
                    p.Shift = production_Plan.Shift;
                    p.LastUpdate = production_Plan.LastUpdate;
                    p.ProductId = production_Plan.ProductId;
                    p.LineId = production_Plan.LineId;

                    _context.Add(p);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {

                    pExistInContext.ProductId = production_Plan.ProductId;
                    pExistInContext.LineId = production_Plan.LineId;
                    //o resto do stop
                    pExistInContext.Goal = production_Plan.Goal;
                    pExistInContext.Name = production_Plan.Name;
                    pExistInContext.InitialDate = production_Plan.InitialDate;
                    pExistInContext.EndDate = production_Plan.EndDate;
                    pExistInContext.Shift = production_Plan.Shift;
                    pExistInContext.LastUpdate = production_Plan.LastUpdate;

                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }


        //---------------Reason
        [HttpPost]
        [Route("ChangeReason")]
        public async Task<ActionResult> ChangeReason([FromBody] JsonReason reason, int? operation)
        {

            var rExistInContext = _context.Reasons.SingleOrDefault(r => r.Id == reason.Id);
            if (rExistInContext == null)
            {
                //Fazer Create
                try
                {
                    Reason r = new Reason();
                    r.Id = reason.Id;
                    r.Description = reason.Description;
                    r.LastUpdate = reason.LastUpdate;

                    _context.Add(r);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    rExistInContext.Description = reason.Description;
                    rExistInContext.LastUpdate = reason.LastUpdate;
                    _context.Update(rExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }          
        }

        //---------------Schedule
        [HttpPost]
        [Route("ChangeSchedule")]
        public async Task<ActionResult> ChangeSchedule([FromBody] JsonSchedule schedule, int? operation)
        {
            var sExistInContext = _context.Schedule_Worker_Lines.SingleOrDefault(s => s.Id == schedule.Id);
            if (sExistInContext == null)
            {
                try
                {
                    Schedule_Worker_Line s = new Schedule_Worker_Line();
                    s.Id = schedule.Id;
                    s.Day = schedule.Day;
                    s.Shift = schedule.Shift;
                    s.LastUpdate = schedule.LastUpdate;
                    s.LineId = schedule.LineId;
                    s.OperatorId = schedule.OperatorId;
                    s.SupervisorId = schedule.SupervisorId;

                    _context.Add(s);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    if (schedule.OperatorId != null)
                    {
                        sExistInContext.OperatorId = schedule.OperatorId;
                    }
                    else
                    {
                        sExistInContext.OperatorId = null;
                    }
                    if (schedule.SupervisorId != null)
                    {
                        sExistInContext.SupervisorId = schedule.SupervisorId;
                    }
                    else
                    {
                        sExistInContext.SupervisorId = null;
                    }

                    sExistInContext.LineId = schedule.LineId;
                    //outros
                    sExistInContext.Day = schedule.Day;
                    sExistInContext.Shift = schedule.Shift;
                    sExistInContext.LastUpdate = schedule.LastUpdate;

                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }

        }

        //---------------Stop
        [HttpPost]
        [Route("ChangeStop")]
        public async Task<ActionResult> ChangeStop([FromBody] JsonStop stop, int? operation)
        {
            var sExistInContext = _context.Stops.SingleOrDefault(s => s.Id == stop.Id);
            if (sExistInContext == null)
            {
                try
                {
                    Stop s = new Stop();
                    s.Id = stop.Id;
                    s.Planned = stop.Planned;
                    s.InitialDate = stop.InitialDate;
                    s.EndDate = stop.EndDate;
                    s.Duration = stop.Duration;
                    s.Shift = stop.Shift;
                    s.LastUpdate = stop.LastUpdate;
                    s.LineId = stop.LineId;
                    s.ReasonId = stop.ReasonId;

                    _context.Add(s);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("stop: " + stop.Id.ToString() + " - Adicionado com suceso");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    if (stop.ReasonId != null)
                    {
                        sExistInContext.ReasonId = stop.ReasonId;
                    }
                    sExistInContext.LineId = stop.LineId;
                    //o resto do stop
                    sExistInContext.Planned = stop.Planned;
                    sExistInContext.InitialDate = stop.InitialDate;
                    sExistInContext.EndDate = stop.EndDate;
                    sExistInContext.Duration = stop.Duration;
                    sExistInContext.Shift = stop.Shift;
                    sExistInContext.LastUpdate = stop.LastUpdate;
                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }

        //---------------Supervisor
        [HttpPost]
        [Route("ChangeSupervisor")]
        public async Task<ActionResult> ChangeSupervisor([FromBody] JsonSupervisor supervisor, int? operation)
        {
            var sExistInContext = _context.Supervisors.SingleOrDefault(s => s.Id == supervisor.Id);
            if (sExistInContext == null)
            {
                try
                {
                    Supervisor s = new Supervisor();
                    s.Id = supervisor.Id;
                    s.WorkerId = supervisor.WorkerId;
                    s.LastUpdate = supervisor.LastUpdate;

                    _context.Add(s);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {
                    sExistInContext.WorkerId = supervisor.WorkerId;
                    sExistInContext.LastUpdate = supervisor.LastUpdate;
                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }

        //---------------Worker
        [HttpPost]
        [Route("ChangeWorker")]
        public async Task<ActionResult> ChangeWorker([FromBody] JsonWorker worker, int? operation)
        {
            var wExistInContext = _context.Workers.SingleOrDefault(w => w.Id == worker.Id);
            if (wExistInContext == null)
            {
                //Fazer Create
                try
                {
                    Worker w = new Worker();
                    w.Id = worker.Id;
                    w.IdFirebase = worker.IdFirebase;
                    w.UserName = worker.UserName;
                    w.Email = worker.Email;
                    w.Role = worker.Role;
                    w.LastUpdate = worker.LastUpdate;

                    _context.Add(w);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {

                    wExistInContext.IdFirebase = worker.IdFirebase;
                    wExistInContext.UserName = worker.UserName;
                    wExistInContext.Email = worker.Email;
                    wExistInContext.Role = worker.Role;
                    wExistInContext.LastUpdate = worker.LastUpdate;

                    _context.Update(wExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }

        //---------------ComponentProduct
        [HttpPost]
        [Route("ChangeComponentProduct")]
        public async Task<ActionResult> ChangeComponentProduct([FromBody] JsonComponentProduct componentProduct, int? operation)
        {
            var cpExistInContext = _context.ComponentProducts.SingleOrDefault(c => c.Id == componentProduct.Id);
            if (cpExistInContext == null)
            {
                //Fazer Create
                try
                {
                    ComponentProduct c = new ComponentProduct();
                    c.Id = componentProduct.Id;
                    c.ProductId = componentProduct.ProductId;
                    c.ComponentId = componentProduct.ComponentId;
                    c.Quantidade = componentProduct.Quantidade;
                    c.LastUpdate = componentProduct.LastUpdate;

                    _context.Add(c);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
            else
            {
                //fazer update
                try
                {

                    cpExistInContext.ProductId = componentProduct.ProductId;
                    cpExistInContext.ComponentId = componentProduct.ComponentId;
                    _context.Update(cpExistInContext);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return BadRequest();
                }
            }
        }


    }
}
