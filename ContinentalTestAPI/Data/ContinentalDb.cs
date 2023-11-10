using Models.ContinentalModels;
using Microsoft.EntityFrameworkCore;
using Models.cdc_Models;
using Models.FunctionModels;

namespace ContinentalTestAPI.Data
{
    public class ContinentalDb : DbContext
    {
        public ContinentalDb(DbContextOptions<ContinentalDb> opt) : base(opt)
        {
            Components = Set<Component>();
            Coordinators = Set<Coordinator>();
            Devices = Set<Device>();
            Lines = Set<Line>();
            Operators = Set<Operator>();
            Products = Set<Product>();
            Productions = Set<Production>();
            Production_Plans = Set<Production_Plan>();
            Reasons = Set<Reason>();
            Schedule_Worker_Lines = Set<Schedule_Worker_Line>();
            Stops = Set<Stop>();
            Supervisors = Set<Supervisor>();
            Workers = Set<Worker>();
            requests = Set<Request>();
            ComponentProducts = Set<ComponentProduct>();
            // Cdcs
            cdc_Stops = Set<CDC_Stop>();
            cdc_Productions = Set<CDC_Production>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContinentalTestDb";
            var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
            var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
            var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";
            optionsBuilder.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");
        }

        public DbSet<Component> Components { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Production_Plan> Production_Plans { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Schedule_Worker_Line> Schedule_Worker_Lines { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<ComponentProduct> ComponentProducts { get; set; }

        //Cdcs
        public DbSet<CDC_Stop> cdc_Stops { get; set; }
        public DbSet<CDC_Production> cdc_Productions { get; set; }
    }
}
