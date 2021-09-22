using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.Application.Protos;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Extensions;
using MontyHallProblemSimulation.Infrastructure.Simulation.Grpc.Comm;
using MontyHallProblemSimulation.Shared.Utility.Extensions;
using System;

namespace MontyHallProblemSimulation.Application.CommandWebHost
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
            services.AddHttpComponents();
            services.AddUtilities();

            var workerHostServiceUrl = this.configuration["CommandWorkerHost"];

            services.AddGrpcClient<SimulationCommand.SimulationCommandClient>(option =>
            {
                option.Address = new Uri(workerHostServiceUrl);

                option.ChannelOptionsActions.Add(x => x.Credentials = ChannelCredentials.Insecure);
            });

            services.AddScoped<IPublishCommandService<SimulationCommand.SimulationCommandClient>, SimulationCommandPublishService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpPipeline();
        }

    }
}
