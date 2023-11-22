using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Models.FunctionModels;

namespace ContextServer.Data
{
    public interface IContextAwareDb 
    {
        public DbSet<Production> Productions { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<LastVerificationRegist> lastVerificationRegists { get; set; }
        public DbSet<MissingComponent> missingComponents { get; set; }
        public DbSet<AlertsHistory> alertsHistories { get; set; }

    }
}
