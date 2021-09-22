using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.ReadSide.ViewModel;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ReadSide.EventHandlers
{
    public class SimulationCreatedEventHandler : IAsyncEventHandler<SimulationCreatedEvent>
    {
        private readonly IReadRepository<SimulationViewModel> readRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public SimulationCreatedEventHandler(
            IReadRepository<SimulationViewModel> readRepository,
            IDateTimeProvider dateTimeProvider)
        {
            this.readRepository = readRepository;
            this.dateTimeProvider = dateTimeProvider;
        }
        public async Task HandleAsync(SimulationCreatedEvent @event)
        {
            if (@event.Simulation != null)
            {
                var viewModel = new SimulationViewModel(
                    @event.Simulation.SimulationId,
                    0,
                    @event.SessionId,
                    @event.SessionId,
                    this.dateTimeProvider.GetUtcDateTime(),
                    this.dateTimeProvider.GetUtcDateTime(),
                    false,
                    @event.Simulation.ChangeDoor,
                    @event.Simulation.SuccessCount,
                    @event.Simulation.FailCount,
                    @event.Simulation.NumberOfSimulations,
                    @event.Simulation.SuccessRatio);

                await this.readRepository.SaveAsync(viewModel);
            }
        }
    }
}
