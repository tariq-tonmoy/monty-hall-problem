using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Shared.SharedDto;
using MontyHallProblemSimulation.Shared.SharedDto.ConfigurationModels;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.HubClient
{
    public class NotificationClientWorker
    {
        private readonly AppSettings appsettings;
        private readonly NotificationOrchestrator orchestrator;
        private readonly IReflectionUtilityProvider reflectionUtilityProvider;
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        private string environment = string.Empty;
        private HubConnection hubConnection;

        private const string VisualStudio = nameof(VisualStudio);
        private const string Docker = nameof(Docker);

        private string GetEnvironmentKey(string environment) => environment.Contains(VisualStudio) ? VisualStudio : environment.Contains(Docker) ? Docker : string.Empty;

        public NotificationClientWorker(IOptions<AppSettings> appsettings, NotificationOrchestrator orchestrator, IReflectionUtilityProvider reflectionUtilityProvider)
        {
            this.appsettings = appsettings.Value;
            this.orchestrator = orchestrator;
            this.reflectionUtilityProvider = reflectionUtilityProvider;
            this.orchestrator.OnEnvironmentChanged += Orchestrator_OnEnvironmentChanged;
            this.orchestrator.OnAppInitialized += Orchestrator_OnAppInitialized;
        }

        private async void Orchestrator_OnAppInitialized()
        {
            try
            {
                await this.ExecuteAsync();
            }
            catch (Exception)
            {
            }
        }

        private async void Orchestrator_OnEnvironmentChanged(string environment)
        {
            if (!this.token.IsCancellationRequested)
            {
                this.environment = environment;
                if (this.hubConnection != null && this.hubConnection.State == HubConnectionState.Connected)
                {
                    await this.hubConnection.StopAsync();
                }
                this.tokenSource.Cancel();
            }
        }

        private void InitializeToken()
        {
            this.tokenSource = new CancellationTokenSource();
            this.token = this.tokenSource.Token;
        }

        protected async Task ExecuteAsync()
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(this.environment))
                {
                    var environmentKey = this.GetEnvironmentKey(environment);
                    var notificationbaseUrl = this.appsettings.ConnectionUrls.First(x => x.Environment == environmentKey).NotificationHost;
                    var notificationUrl = $"{notificationbaseUrl}/hubs";

                    try
                    {
                        this.hubConnection = new HubConnectionBuilder()
                                            .WithUrl(notificationUrl)
                                            .WithAutomaticReconnect()
                                            .Build();

                        this.hubConnection.On<string, string>("ReceiveEvent", (eventPayload, assemblyName) =>
                        {
                            ActionType actionType = ActionType.BUSINESS_RULE_VIOLATION;
                            SimulationEventDto simulation = null;
                            if (assemblyName == this.reflectionUtilityProvider.GetFullyQualifiedAssemblyName(typeof(SimulationCreatedEvent)))
                            {
                                actionType = ActionType.CREATE;
                                simulation = NotificationPayloadHelper.GetDomainEventFromNotificationPayload<SimulationCreatedEvent>(eventPayload, assemblyName).Simulation;

                            }
                            else if (assemblyName == this.reflectionUtilityProvider.GetFullyQualifiedAssemblyName(typeof(SimulationRerunEvent)))
                            {
                                actionType = ActionType.RERUN;
                                simulation = NotificationPayloadHelper.GetDomainEventFromNotificationPayload<SimulationRerunEvent>(eventPayload, assemblyName).Simulation;
                            }
                            else if (assemblyName == this.reflectionUtilityProvider.GetFullyQualifiedAssemblyName(typeof(SimulationDeactivatedEvent)))
                            {
                                actionType = ActionType.DEACTIVATE;
                            }

                            this.orchestrator.ActionPerformed(actionType, simulation);
                        });

                        await this.hubConnection.StartAsync();


                    }
                    catch (Exception)
                    {
                    }
                }

                try
                {
                    this.InitializeToken();
                    await Task.Delay(-1, this.token);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
