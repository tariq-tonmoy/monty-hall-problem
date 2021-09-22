using MontyHallProblemSimulation.Application.Protos;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Grpc.Comm
{
    public class SimulationCommandPublishService : IPublishCommandService<SimulationCommand.SimulationCommandClient>
    {
        private readonly SimulationCommand.SimulationCommandClient client;
        private readonly IReflectionUtilityProvider reflectionUtility;

        public SimulationCommandPublishService(SimulationCommand.SimulationCommandClient client, IReflectionUtilityProvider reflectionUtility)
        {
            this.client = client;
            this.reflectionUtility = reflectionUtility;
        }

        public async Task PublishMessageAsync<TCommand>(TCommand command) where TCommand : Command
        {
            var messagePayload = JsonConvert.SerializeObject(command);
            await this.client.ApplyCommandAsync(new CommandModel()
            {
                CommandPayload = messagePayload,
                AssemblyName = this.reflectionUtility.GetFullyQualifiedAssemblyName(command.GetType()),
            });
        }
    }
}
