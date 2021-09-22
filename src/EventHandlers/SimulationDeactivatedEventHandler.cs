using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.ReadSide.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ReadSide.EventHandlers
{
    public class SimulationDeactivatedEventHandler : IAsyncEventHandler<SimulationDeactivatedEvent>
    {
        private readonly IReadRepository<SimulationViewModel> readRepository;

        public SimulationDeactivatedEventHandler(IReadRepository<SimulationViewModel> readRepository)
        {
            this.readRepository = readRepository;
        }

        public async Task HandleAsync(SimulationDeactivatedEvent @event)
        {
            if (@event.SimulationDto != null)
            {
                var viewModel = this.readRepository.GetByFilter(x => x.Id == @event.SimulationDto.SimulationId).FirstOrDefault();
                if (viewModel != null)
                {
                    await this.readRepository.DeleteAsync(viewModel);
                }
            }
        }
    }
}
