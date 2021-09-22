using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository.Sqlite;
using MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite;
using MontyHallProblemSimulation.ReadSide.EventHandlers;
using MontyHallProblemSimulation.ReadSide.ViewModel;
using MontyHallProblemSimulation.Shared.Utility.Extensions;
using SimulationService.Infrastructure.Core.Extensions;

namespace MontyHallProblemSimulation.ReadSide.EventWorkerHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUtilities();
            services.AddGrpc();

            services.AddMessageHandlers()
                    .AddEventHandler<SimulationCreatedEvent>(typeof(SimulationCreatedEventHandler))
                    .AddEventHandler<SimulationRerunEvent>(typeof(SimulationRerunEventHandler))
                    .AddEventHandler<SimulationDeactivatedEvent>(typeof(SimulationDeactivatedEventHandler));

            services.AddScoped<IDbConnectionSettingsProvider, SqliteDbConnectionSettingsProvider>();
            services.AddEntityFrameworkSqlite().AddDbContext<SimulationViewModelDbContext>();
            services.AddScoped<IReadRepository<SimulationViewModel>, SqliteReadRepository<SimulationViewModel, SimulationViewModelDbContext>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SimulationEventHandlerService>();
            });
        }
    }
}
