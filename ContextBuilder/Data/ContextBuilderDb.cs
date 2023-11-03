using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Models.FunctionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextBuilder.Data
{
    public class ContextBuilderDb : DbContext
    {
        public ContextBuilderDb(DbContextOptions<ContextBuilderDb> opt) : base(opt)
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
        public DbSet<Request> Requests { get; set; }
        public DbSet<MissingComponent> missingComponents { get; set; }
    }
}
