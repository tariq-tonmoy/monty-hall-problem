using Microsoft.EntityFrameworkCore;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite
{
    public class SimulationAggregateRootDbContext : DbContext
    {
        private readonly IDbConnectionSettingsProvider connectionSettingsProvider;

        public SimulationAggregateRootDbContext(DbContextOptions<SimulationAggregateRootDbContext> options, IDbConnectionSettingsProvider connectionSettingsProvider)
            : base(options)
        {
            this.connectionSettingsProvider = connectionSettingsProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConnectionString = this.connectionSettingsProvider.GetDbConnectionString("StateDatabaseConnectionString");
            optionsBuilder.UseSqlite(dbConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SimulationAggregateRootConfiguration());
        }
    }
}
