using Models.ContinentalModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ContinentalTestDb.Data
{
    public class ContinentalTestDbContext : DbContext
    {
        public ContinentalTestDbContext(DbContextOptions<ContinentalTestDbContext> opt) : base(opt)
        {
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContinentalTestDb";
            var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
            var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
            var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";
            optionsBuilder.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");
        }

        public DbSet<Component> Components{ get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Production> Productions{ get; set; }
        public DbSet<Production_Plan> Production_Plans { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Schedule_Worker_Line> Schedule_Worker_Lines { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
