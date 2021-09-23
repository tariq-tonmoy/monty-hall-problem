using Microsoft.Extensions.Options;
using MontyHallProblemSimulation.Application.Commands;
using MontyHallProblemSimulation.ClientSide.WebClient.Abstractions;
using MontyHallProblemSimulation.ClientSide.WebClient.Helpers;
using MontyHallProblemSimulation.Shared.SharedDto;
using MontyHallProblemSimulation.Shared.SharedDto.ConfigurationModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.WebClient
{
    public class PublishSimulateCommandService : IPublishSimulateCommandService
    {
        private readonly IOptions<AppSettings> appsettings;
        private readonly IHttpHelper httpHelper;

        public PublishSimulateCommandService(IOptions<AppSettings> appsettings, IHttpHelper httpHelper)
        {
            this.appsettings = appsettings;
            this.httpHelper = httpHelper;
        }

        public async Task<bool> PublishCreateSimulationCommandAsync(long numberOfSimulations, bool changeDoor, string environment)
        {
            CreateSimulationsCommand command = new CreateSimulationsCommand()
            {
                CorrelationId = Guid.NewGuid(),
                SessionId = WebClientHelper.SessionId,
                Simulations = new List<CreateSimulationDto>()
                {
                    new CreateSimulationDto()
                    {
                        ChangeDoor = changeDoor,
                        NumberOfSimulations = numberOfSimulations,
                        SimulationId = Guid.NewGuid(),
                    }
                },
            };

            var stringfiedPayload = JsonConvert.SerializeObject(command);
            string env = WebClientHelper.GetEnvironmentKey(environment);

            if (!string.IsNullOrWhiteSpace(env))
            {
                var commandBaseUrl = this.appsettings.Value.ConnectionUrls.First(x => x.Environment == env)?.CommandWebHost;
                var createSimulationUrl = $"{commandBaseUrl}/Simulation/CreateSimulations";
                var response = await this.httpHelper.SendPostMethodAsync(createSimulationUrl, stringfiedPayload);

                return response.IsSuccessful;
            }


            return false;
        }

        public async Task<bool> PublishDeactivateSimulationCommandAsync(Guid simulationId, string environment)
        {
            var command = new RerunSimulationsCommand()
            {
                CorrelationId = Guid.NewGuid(),
                SessionId = WebClientHelper.SessionId,
                Simulations = new List<RerunSimulationDto>()
                {
                    new RerunSimulationDto()
                    {
                        SimulationId = simulationId,
                    }
                },
            };

            var stringfiedPayload = JsonConvert.SerializeObject(command);
            string env = WebClientHelper.GetEnvironmentKey(environment);

            if (!string.IsNullOrWhiteSpace(env))
            {
                var commandBaseUrl = this.appsettings.Value.ConnectionUrls.First(x => x.Environment == env)?.CommandWebHost;
                var rerunSimulationUrl = $"{commandBaseUrl}/Simulation/DeactivateSimulations";
                var response = await this.httpHelper.SendPostMethodAsync(rerunSimulationUrl, stringfiedPayload);

                return response.IsSuccessful;
            }


            return false;
        }

        public async Task<bool> PublishRerunSimulationCommandAsync(Guid simulationId, string environment)
        {
            var command = new DeactivateSimulationsCommand()
            {
                CorrelationId = Guid.NewGuid(),
                SessionId = WebClientHelper.SessionId,
                Simulations = new List<DeactivateSimulationDto>()
                {
                    new DeactivateSimulationDto()
                    {
                        SimulationId = simulationId,
                    }
                },
            };

            var stringfiedPayload = JsonConvert.SerializeObject(command);
            string env = WebClientHelper.GetEnvironmentKey(environment);

            if (!string.IsNullOrWhiteSpace(env))
            {
                var commandBaseUrl = this.appsettings.Value.ConnectionUrls.First(x => x.Environment == env)?.CommandWebHost;
                var deactivateSimulationUrl = $"{commandBaseUrl}/Simulation/RerunSimulations";
                var response = await this.httpHelper.SendPostMethodAsync(deactivateSimulationUrl, stringfiedPayload);

                return response.IsSuccessful;
            }


            return false;
        }
    }
}
