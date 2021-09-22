using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.ReadSide.ViewModel;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ReadSide.EventHandlers
{
    public class SimulationRerunEventHandler : IAsyncEventHandler<SimulationRerunEvent>
    {
        private readonly IReadRepository<SimulationViewModel> readRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public SimulationRerunEventHandler(
            IReadRepository<SimulationViewModel> readRepository,
            IDateTimeProvider dateTimeProvider)
        {
            this.readRepository = readRepository;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task HandleAsync(SimulationRerunEvent @event)
        {
            if (@event.Simulation != null)
            {
                var viewModel = this.readRepository.GetByFilter(x => x.Id == @event.Simulation.SimulationId).FirstOrDefault();

                if (viewModel != null)
                {
                    viewModel.UpdateSimulationViewModel(@event.Simulation, @event.SessionId, this.dateTimeProvider);
                    await this.readRepository.UpdateAsync(viewModel);
                }
            }
        }
    }
}
