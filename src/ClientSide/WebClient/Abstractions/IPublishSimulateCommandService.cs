using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.WebClient.Abstractions
{
    public interface IPublishSimulateCommandService
    {
        Task<bool> PublishCreateSimulationCommandAsync(long numberOfSimulations, bool changeDoor, string environment);

        Task<bool> PublishRerunSimulationCommandAsync(Guid simulationId, string environment);

        Task<bool> PublishDeactivateSimulationCommandAsync(Guid simulationId, string environment);
    }
}
