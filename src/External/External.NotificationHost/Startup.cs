using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.External.NotificationHost.Hubs;
using MontyHallProblemSimulation.Infrastructure.Extensions;

namespace MontyHallProblemSimulation.External.NotificationHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpComponents();
            services.AddSignalR();
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpPipeline();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<NotificationEventHandlerService>();
                endpoints.MapHub<NotificationHub>("/hubs");
            });
        }
    }
}
