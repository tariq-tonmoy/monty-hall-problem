using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.Application.CommandHandlers;
using MontyHallProblemSimulation.Application.Commands;
using MontyHallProblemSimulation.Application.Protos;
using MontyHallProblemSimulation.Domain.DomainService.Extensions;
using MontyHallProblemSimulation.Domain.SimulationAggregateRoot;
using MontyHallProblemSimulation.External.Protos;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Core.Service.Imps;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository.Sqlite;
using MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite;
using MontyHallProblemSimulation.Infrastructure.Simulation.Grpc.Comm;
using MontyHallProblemSimulation.Shared.Utility.Extensions;
using SimulationService.Infrastructure.Core.Extensions;
using System;

namespace MontyHallProblemSimulation.Application.CommandWorkerHost
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUtilities();
            services.AddGrpc();
            services.AddSimulationDomainServices();

            services.AddMessageHandlers()
                    .AddCommandHandler<CreateSimulationsCommand>(typeof(CreateSimulationsCommandHandler))
                    .AddCommandHandler<RerunSimulationsCommand>(typeof(RerunSimulationsCommandHandler))
                    .AddCommandHandler<DeactivateSimulationsCommand>(typeof(DeactivateSimulationsCommandHandler));

            services.AddScoped<IDbConnectionSettingsProvider, SqliteDbConnectionSettingsProvider>();
            services.AddEntityFrameworkSqlite().AddDbContext<SimulationAggregateRootDbContext>();
            services.AddScoped<IAggregateRootRepository<SimulationAggregateRoot>, SqliteAggregateRootRepository<SimulationAggregateRoot, SimulationAggregateRootDbContext>>();

            var eventWorkerHostServiceUrl = this.configuration["EventWorkerHost"];

            services.AddGrpcClient<SimulationEvent.SimulationEventClient>(option =>
            {
                option.Address = new Uri(eventWorkerHostServiceUrl);

                option.ChannelOptionsActions.Add(x => x.Credentials = ChannelCredentials.Insecure);
            });

            var notificationWorkerHostServiceUrl = this.configuration["NotificationHost"];

            services.AddGrpcClient<NotificationEvent.NotificationEventClient>(option =>
            {
                option.Address = new Uri(notificationWorkerHostServiceUrl);

                option.ChannelOptionsActions.Add(x => x.Credentials = ChannelCredentials.Insecure);
            });


            services.AddScoped<IPublishEventsAsBulkService, PublishEventAsBulkOnGrpcService>();

            services.AddScoped<IPublishEventService<SimulationEvent.SimulationEventClient>, SimulationEventPublishService>();
            services.AddScoped<IPublishEventBase, SimulationEventPublishService>();

            services.AddScoped<IPublishEventService<NotificationEvent.NotificationEventClient>, NotificationEventPublishService>();
            services.AddScoped<IPublishEventBase, NotificationEventPublishService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SimulationCommandHandlerService>();
            });
        }
    }
}
