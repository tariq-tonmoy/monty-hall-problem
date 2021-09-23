using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.ClientSide.HubClient;
using MontyHallProblemSimulation.ClientSide.WebClient;
using MontyHallProblemSimulation.ClientSide.WebClient.Abstractions;
using MontyHallProblemSimulation.ClientSide.WebClient.Helpers;
using MontyHallProblemSimulation.Shared.SharedDto.ConfigurationModels;
using MontyHallProblemSimulation.Shared.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FrontEndClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider serviceProvider { get; private set; }

        public IConfiguration configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddUtilities();
            services.AddSingleton<NotificationOrchestrator>();
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<NotificationClientWorker>();
            services.AddSingleton<IHttpHelper, HttpHelper>();
            services.AddSingleton<IQueryClient, QueryClientService>();
            services.AddSingleton<IPublishSimulateCommandService, PublishSimulateCommandService>();
            services.AddTransient(typeof(MainWindow));
        }
    }
}
