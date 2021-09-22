using Microsoft.EntityFrameworkCore;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite
{
    public class SimulationViewModelDbContext : DbContext
    {
        private readonly IDbConnectionSettingsProvider connectionSettingsProvider;

        public SimulationViewModelDbContext(DbContextOptions<SimulationViewModelDbContext> options, IDbConnectionSettingsProvider connectionSettingsProvider)
            : base(options)
        {
            this.connectionSettingsProvider = connectionSettingsProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConnectionString = this.connectionSettingsProvider.GetDbConnectionString("ReadDatabaseConnectionString");
            optionsBuilder.UseSqlite(dbConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SimulationViewModelConfiguration());
        }
    }
}
