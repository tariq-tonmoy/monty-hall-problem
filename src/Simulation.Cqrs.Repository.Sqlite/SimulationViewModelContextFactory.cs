using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository.Sqlite;
using System.IO;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite
{
    public class SimulationViewModelContextFactory : IDesignTimeDbContextFactory<SimulationViewModelDbContext>
    {
        public SimulationViewModelDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EventWorkerHost"))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{args[0]}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SimulationViewModelDbContext>();
            SqliteDbConnectionSettingsProvider settingsProvider = new SqliteDbConnectionSettingsProvider(config);

            optionsBuilder.UseSqlite(config.GetSection("ReadDatabaseConnectionString")?.Value);

            return new SimulationViewModelDbContext(optionsBuilder.Options, settingsProvider);
        }
    }
}
