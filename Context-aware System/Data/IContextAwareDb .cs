using Microsoft.EntityFrameworkCore;
using Models.ContextModels;

namespace Context_aware_System.Data
{
    public interface IContextAwareDb 
    {
        public DbSet<Production> Productions { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<LastVerificationRegist> lastVerificationRegists { get; set; }
    }
}
