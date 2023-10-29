using ContextAcquisition.Data;
using ContextAcquisition.Services;
using ContinentalTestDb.Data;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;

namespace Testes
{
    public class ContextAcquisitionTests
    {
        public readonly ContextAcquisitonDb _context = new ContextAcquisitonDb();
        public readonly ContinentalTestDbContext _contextContinental;

        public ContextAcquisitionTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContinentalTestDbContext>();

            var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContinentalTestDb";
            var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
            var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
            var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";

            optionsBuilder.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");

            _contextContinental = new ContinentalTestDbContext(optionsBuilder.Options);
        }

    //    //components
    //    [Fact]
    //    public void ComponentTest()
    //    {
    //        var componentsContinental = _contextContinental.Components.ToList();
    //        var componentsContext = _context.Components.ToList();

    //        bool teste = true;
    //        foreach (var component in componentsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!componentsContext.Exists(c=> c.Id == component.Id && c.Name == component.Name 
    //            && c.Reference == component.Reference && c.Category == component.Category))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }
    //    //coordinatotrs

    //    [Fact]
    //    public void CoordinatorTest()
    //    {
    //        var CoordinatorsContinental = _contextContinental.Coordinators.ToList();
    //        var CoordinatorsContext = _context.Coordinators.ToList();

    //        bool teste = true;
    //        foreach (var Coordinator in CoordinatorsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!CoordinatorsContext.Exists(c => c.Id == Coordinator.Id &&c.WorkerId == Coordinator.WorkerId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //devices

    //    [Fact]
    //    public void DevicesTest()
    //    {
    //        var DevicesContinental = _contextContinental.Devices.ToList();
    //        var DevicesContext = _context.Devices.ToList();

    //        bool teste = true;
    //        foreach (var device in DevicesContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!DevicesContext.Exists(d => d.Id == device.Id && d.Type == device.Type && d.LineId == device.LineId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Lines
    //    [Fact]
    //    public void LinesTest()
    //    {
    //        var LinesContinental = _contextContinental.Lines.ToList();
    //        var LinesContext = _context.Lines.ToList();

    //        bool teste = true;
    //        foreach (var line in LinesContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!LinesContext.Exists(l => l.Id == line.Id && l.Name == line.Name && l.Priority == line.Priority && l.CoordinatorId == line.CoordinatorId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Operators
    //    [Fact]
    //    public void OperatorsTest()
    //    {
    //        var OperatorsContinental = _contextContinental.Operators.ToList();
    //        var OperatorsContext = _context.Operators.ToList();

    //        bool teste = true;
    //        foreach (var ope in OperatorsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!OperatorsContext.Exists(o => o.Id == ope.Id && o.WorkerId == ope.WorkerId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Products
    //    [Fact]
    //    public void ProductsTest()
    //    {
    //        var ProductsContinental = _contextContinental.Products.ToList();
    //        var ProductsContext = _context.Products.ToList();

    //        bool teste = true;
    //        foreach (var product in ProductsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!ProductsContext.Exists(p => p.Id == product.Id && p.Name == product.Name && p.LabelReference== product.LabelReference && p.Cycle.Equals(product.Cycle)))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Productions
    //    [Fact]
    //    public void ProductionsTest()
    //    {
    //        var ProductionsContinental = _contextContinental.Productions.ToList();
    //        var ProductionsContext = _context.Productions.ToList();

    //        bool teste = true;
    //        foreach (var production in ProductionsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!ProductionsContext.Exists(p => p.Id == production.Id && p.Hour == production.Hour 
    //            && p.Day.Equals(production.Day) && p.Quantity == production.Quantity 
    //            && p.Production_PlanId == production.Production_PlanId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Production_Plans
    //    [Fact]
    //    public void Production_PlansTest()
    //    {
    //        var Production_PlansContinental = _contextContinental.Production_Plans.ToList();
    //        var Production_PlansContext = _context.Production_Plans.ToList();

    //        bool teste = true;
    //        foreach (var production_Plan in Production_PlansContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!Production_PlansContext.Exists(p => p.Id == production_Plan.Id && p.Goal == production_Plan.Goal 
    //            && p.Name == production_Plan.Name && p.InitialDate.Equals(production_Plan.InitialDate)
    //            && p.EndDate.Equals(production_Plan.EndDate) && p.Shift == production_Plan.Shift && p.ProductId == production_Plan.ProductId
    //            && p.LineId == production_Plan.LineId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Reasons
    //    [Fact]
    //    public void ReasonsTest()
    //    {
    //        var ReasonsContinental = _contextContinental.Reasons.ToList();
    //        var ReasonsContext = _context.Reasons.ToList();

    //        bool teste = true;
    //        foreach (var reason in ReasonsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!ReasonsContext.Exists(r => r.Id == reason.Id && r.Description == reason.Description))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Reasons
    //    [Fact]
    //    public void Schedule_Worker_LinesTest()
    //    {
    //        var Schedule_Worker_LinesContinental = _contextContinental.Schedule_Worker_Lines.ToList();
    //        var Schedule_Worker_LinesContext = _context.Schedule_Worker_Lines.ToList();

    //        bool teste = true;
    //        foreach (var schedule_Worker_Line in Schedule_Worker_LinesContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!Schedule_Worker_LinesContext.Exists(s => s.Id == schedule_Worker_Line.Id && s.Day.Equals(schedule_Worker_Line.Day) 
    //            && s.Shift == schedule_Worker_Line.Shift && s.LineId == schedule_Worker_Line.LineId
    //            && s.OperatorId == schedule_Worker_Line.OperatorId && s.SupervisorId == schedule_Worker_Line.SupervisorId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Stops
    //    [Fact]
    //    public void StopsTest()
    //    {
    //        var StopsContinental = _contextContinental.Stops.ToList();
    //        var StopsContext = _context.Stops.ToList();

    //        bool teste = true;
    //        foreach (var stop in StopsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!StopsContext.Exists(s => s.Id == stop.Id && s.Planned == stop.Planned
    //            && s.InitialDate.Equals(stop.InitialDate) && s.EndDate.Equals(stop.EndDate)
    //            && s.Duration.Equals(stop.Duration) && s.Shift == stop.Shift 
    //            && s.LineId == stop.LineId && s.ReasonId == stop.ReasonId))
    //            { 
                
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Supervisors
    //    [Fact]
    //    public void SupervisorsTest()
    //    {
    //        var SupervisorsContinental = _contextContinental.Supervisors.ToList();
    //        var SupervisorsContext = _context.Supervisors.ToList();

    //        bool teste = true;
    //        foreach (var supervisor in SupervisorsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!SupervisorsContext.Exists(s => s.Id == supervisor.Id && s.WorkerId == supervisor.WorkerId))
    //            {

    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //Workers
    //    [Fact]
    //    public void WorkersTest()
    //    {
    //        var WorkersContinental = _contextContinental.Workers.ToList();
    //        var WorkersContext = _context.Workers.ToList();

    //        bool teste = true;
    //        foreach (var worker in WorkersContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!WorkersContext.Exists(w => w.Id == worker.Id && w.IdFirebase == worker.IdFirebase
    //            && w.UserName == worker.UserName && w.Email == worker.Email && w.Role == worker.Role))
    //            {

    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }

    //    //ComponentProducts
    //    [Fact]
    //    public void ComponentProducts()
    //    {
    //        var ComponentProductsContinental = _contextContinental.ComponentProducts.ToList();
    //        var ComponentProductsContext = _context.ComponentProducts.ToList();

    //        bool teste = true;
    //        foreach (var compProduct in ComponentProductsContinental)
    //        {
    //            //tem de estar na bd do contexto senão retorna erro
    //            if (!ComponentProductsContext.Exists(c => c.Id == compProduct.Id && c.ComponentId == compProduct.ComponentId && c.ProductId == compProduct.ProductId))
    //            {
    //                teste = false;
    //            }
    //        }

    //        Assert.True(teste);
    //    }
    }
}