using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace MontyHallProblemSimulation.External.NotificationHost
{
    class Program
    {
        private const string AspnetcoreEnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";

        static void Main(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(AspnetcoreEnvironmentVariableName)}.json")
                .Build();

            CreateHostBuilder(args, configurationRoot).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, configurationBuilder) =>
            {
                configurationBuilder.AddConfiguration(configuration);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {

                    var httpPort = configuration.GetValue<int>("HttpPort");
                    var http2Port = configuration.GetValue<int>("Http2Port");

                    serverOptions.Listen(IPAddress.Any, http2Port, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
                    serverOptions.Listen(IPAddress.Any, httpPort, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1;
                    });
                });

                webBuilder
                    .UseStartup<Startup>();
            });
    }
}
