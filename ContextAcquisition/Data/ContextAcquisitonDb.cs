using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.ContextModels;
using Models.FunctionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Data
{
    public class ContextAcquisitonDb : DbContext, IContextAcquisitonDb
    {
        public ContextAcquisitonDb()
        {
            Productions = Set<Production>();
            Stops = Set<Stop>();
            LastVerificationRegists = Set<LastVerificationRegist>();
            alertsHistories = Set<AlertsHistory>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContextDb";
            var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? ".\\SQLEXPRESS";
            optionsBuilder.UseSqlServer($"Server={dbhost};Database={dbname};Trusted_Connection=True;");
        }

        public DbSet<Production> Productions { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<LastVerificationRegist> LastVerificationRegists { get; set; }
        public DbSet<AlertsHistory> alertsHistories { get; set; }


        //Funções
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public new EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            base.Update(entity);
        }
    }
}
