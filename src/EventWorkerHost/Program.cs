using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace MontyHallProblemSimulation.ReadSide.EventWorkerHost
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
                webBuilder.UseStartup<Startup>();
            });
    }
}
