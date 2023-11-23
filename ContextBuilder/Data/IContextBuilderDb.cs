using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.ContextModels;
using Models.FunctionModels;

namespace ContextBuilder.Data
{
    public interface IContextBuilderDb
    {
        public DbSet<Production> Productions { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<MissingComponent> missingComponents { get; set; }
        public DbSet<AlertsHistory> alertsHistories { get; set; }

        public Task<int> SaveChangesAsync();
        public EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

    }
}
