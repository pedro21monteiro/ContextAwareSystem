using ContextAcquisition.Data;
using ContextAcquisition.Services;
using ContinentalTestDb.Data;
using Microsoft.EntityFrameworkCore;

namespace Testes
{

    public class ContextAcquisitionTests
    {
        public readonly ContextAcquisitonDb _context = new ContextAcquisitonDb();
        public readonly ContinentalTestDbContext _contextContinental;
        public readonly Logic _logic = new Logic();

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


        //----------------------------------------------Integration Tests----------------------------------

        //productions
        [Fact]
        public async Task Productions_Integration_Test()
        {
            var productionsContinental = await _contextContinental.Productions.ToListAsync();
            var productionsContext = await _context.Productions.ToListAsync();

            Assert.All(productionsContinental, production =>
            {
                Assert.True(productionsContext.Any(p =>
                    p.Id == production.Id &&
                    p.Hour == production.Hour &&
                    p.Day.Equals(production.Day) &&
                    p.Quantity == production.Quantity &&
                    p.Production_PlanId == production.Production_PlanId),
                    $"A produção com Id {production.Id} não foi encontrada na base de dados do contexto.");
            });
        }

        //Stops
        [Fact]
        public async Task Stops_Integration_Test()
        {
            var stopsContinental = await _contextContinental.Stops.ToListAsync();
            var stopsContext = await _context.Stops.ToListAsync();

            Assert.All(stopsContinental, stop =>
            {
                Assert.True(stopsContext.Any(s =>
                    s.Id == stop.Id &&
                    s.Planned == stop.Planned &&
                    s.InitialDate.Equals(stop.InitialDate) &&
                    s.EndDate.Equals(stop.EndDate) &&
                    s.Duration.Equals(stop.Duration) &&
                    s.Shift == stop.Shift &&
                    s.LineId == stop.LineId &&
                    s.ReasonId == stop.ReasonId),
                    $"A paragem com Id {stop.Id} não foi encontrada na base de dados do contexto.");
            });
        }



    }
}