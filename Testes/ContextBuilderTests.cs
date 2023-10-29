using ContextAcquisition.Data;
using ContextAcquisition.Services;
using ContinentalTestDb.Data;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;

namespace Testes
{
    public class ContextBuilderTests
    {
        public readonly ContextAcquisitonDb _context = new ContextAcquisitonDb();
        public readonly ContinentalTestDbContext _contextContinental;

        public ContextBuilderTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContinentalTestDbContext>();

            var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContinentalTestDb";
            var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
            var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
            var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";

            optionsBuilder.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");

            _contextContinental = new ContinentalTestDbContext(optionsBuilder.Options);
        }

        ////requests
        //[Fact]
        //public void RequestsTest()
        //{
        //    var requestsContinental = _contextContinental.Requests.ToList();
        //    var requestsContext = _context.Requests.ToList();

        //    bool teste = true;
        //    foreach (var request in requestsContinental)
        //    {
        //        //tem de estar na bd do contexto senão retorna erro
        //        if (!requestsContext.Exists(r => r.Type == request.Type && r.Date.Equals(request.Date)
        //        && r.WorkerId == request.WorkerId))
        //        {
        //            teste = false;
        //        }
        //    }

        //    Assert.True(teste);
        //}
    }
}