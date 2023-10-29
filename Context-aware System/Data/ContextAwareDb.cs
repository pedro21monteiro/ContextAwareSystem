using Context_aware_System.Data;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;

namespace ContextServer.Data
{
    public class ContextAwareDb : DbContext, IContextAwareDb
    {
        public ContextAwareDb(DbContextOptions<ContextAwareDb> opt) : base(opt)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContextDb";
            var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
            var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
            var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";
            optionsBuilder.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");
        }

        public DbSet<Production> Productions { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<LastVerificationRegist> lastVerificationRegists { get; set; }
    }
}
