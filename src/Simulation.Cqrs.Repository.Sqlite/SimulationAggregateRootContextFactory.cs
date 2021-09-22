using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository.Sqlite;
using System.IO;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite
{
    public class SimulationAggregateRootContextFactory : IDesignTimeDbContextFactory<SimulationAggregateRootDbContext>
    {
        public SimulationAggregateRootDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CommandWorkerHost"))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{args[0]}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SimulationAggregateRootDbContext>();
            SqliteDbConnectionSettingsProvider settingsProvider = new SqliteDbConnectionSettingsProvider(config);

            optionsBuilder.UseSqlite(config.GetSection("StateDatabaseConnectionString")?.Value);

            return new SimulationAggregateRootDbContext(optionsBuilder.Options, settingsProvider);
        }
    }
}
