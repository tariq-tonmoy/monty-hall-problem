using Microsoft.Extensions.Configuration;
using MontyHallProblemSimulation.Application.Commands;
using MontyHallProblemSimulation.Domain.DomainService.Abstractions;
using MontyHallProblemSimulation.Domain.SimulationAggregateRoot;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Application.CommandHandlers
{
    public class CreateSimulationsCommandHandler : IAsyncCommandHandler<CreateSimulationsCommand>
    {
        private readonly IAggregateRootRepository<SimulationAggregateRoot> aggregateRootRepository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMontyHallProblemSimulationService simulationService;
        private readonly IPublishEventsAsBulkService publishEventService;
        private readonly int batchSize;


        public CreateSimulationsCommandHandler(
            IAggregateRootRepository<SimulationAggregateRoot> aggregateRootRepository,
            IConfiguration configuration,
            IDateTimeProvider dateTimeProvider,
            IMontyHallProblemSimulationService simulationService,
            IPublishEventsAsBulkService publishEventService)
        {
            this.aggregateRootRepository = aggregateRootRepository;
            this.dateTimeProvider = dateTimeProvider;
            this.simulationService = simulationService;
            this.publishEventService = publishEventService;
            this.batchSize = configuration.GetValue<int>("BatchSize");
        }

        public async Task<CommandRespose> HandleAsync(CreateSimulationsCommand command)
        {
            if (command.Simulations != null && command.Simulations.Any())
            {
                foreach (var simulation in command.Simulations)
                {
                    SimulationAggregateRoot aggregateRoot = new SimulationAggregateRoot();
                    aggregateRoot.CreateSimulation(simulation, command.SessionId, command.CorrelationId, this.batchSize, this.dateTimeProvider, this.aggregateRootRepository, this.simulationService);
                    await this.aggregateRootRepository.SaveAsync(aggregateRoot);

                    await this.publishEventService.PublishBulkEvents(aggregateRoot.DomainEvents);
                }
            }

            return new CommandRespose();
        }
    }
}
