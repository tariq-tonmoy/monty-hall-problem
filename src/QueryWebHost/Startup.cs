using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository.Sqlite;
using MontyHallProblemSimulation.Infrastructure.Extensions;
using MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite;
using MontyHallProblemSimulation.ReadSide.QueryWebHost.Abstractions;
using MontyHallProblemSimulation.ReadSide.QueryWebHost.Repositories;

namespace MontyHallProblemSimulation.ReadSide.QueryWebHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpComponents();
            services.AddSingleton<IDbConnectionSettingsProvider, SqliteDbConnectionSettingsProvider>();
            services.AddEntityFrameworkSqlite().AddDbContext<SimulationViewModelDbContext>();
            services.AddScoped<IQueryRepository, SimulationQueryRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpPipeline();
        }
    }
}
