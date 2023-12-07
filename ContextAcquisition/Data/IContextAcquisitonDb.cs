using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.ContextModels;
using Models.FunctionModels;

namespace ContextAcquisition.Data
{
    public interface IContextAcquisitonDb
    {
        public DbSet<Production> Productions { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<AlertsHistory> alertsHistories { get; set; }
        public DbSet<LastVerificationRegist> LastVerificationRegists { get; set; }

        public Task<int> SaveChangesAsync();
        public EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
    }
}
