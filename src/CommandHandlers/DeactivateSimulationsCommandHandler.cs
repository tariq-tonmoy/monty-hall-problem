using MontyHallProblemSimulation.Application.Commands;
using MontyHallProblemSimulation.Domain.SimulationAggregateRoot;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Application.CommandHandlers
{
    public class DeactivateSimulationsCommandHandler : IAsyncCommandHandler<DeactivateSimulationsCommand>
    {
        private readonly IAggregateRootRepository<SimulationAggregateRoot> aggregateRootRepository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IPublishEventsAsBulkService publishEventService;

        public DeactivateSimulationsCommandHandler(
            IAggregateRootRepository<SimulationAggregateRoot> aggregateRootRepository,
            IDateTimeProvider dateTimeProvider,
            IPublishEventsAsBulkService publishEventService)
        {
            this.aggregateRootRepository = aggregateRootRepository;
            this.dateTimeProvider = dateTimeProvider;
            this.publishEventService = publishEventService;
        }

        public async Task<CommandRespose> HandleAsync(DeactivateSimulationsCommand command)
        {
            if (command.Simulations != null && command.Simulations.Any())
            {
                foreach (var simulation in command.Simulations)
                {
                    var aggregateRoot = await this.aggregateRootRepository.GetByIdAsync(simulation.SimulationId);

                    if (aggregateRoot != null)
                    {
                        aggregateRoot.DeactivateSimulation(simulation, command.SessionId, command.CorrelationId, this.dateTimeProvider);
                    }
                    else
                    {
                        aggregateRoot = new SimulationAggregateRoot();
                        aggregateRoot.HandleMissingSimulation(simulation.SimulationId, command.SessionId, command.CorrelationId);
                    }

                    await this.aggregateRootRepository.UpdateAsync(aggregateRoot);

                    await this.publishEventService.PublishBulkEvents(aggregateRoot.DomainEvents);
                }
            }

            return new CommandRespose();
        }
    }
}
