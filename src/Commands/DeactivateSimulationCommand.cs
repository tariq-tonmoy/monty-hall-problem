using MontyHallProblemSimulation.Infrastructure.Core;
using System;

namespace MontyHallProblemSimulation.Application.Commands
{
    public class DeactivateSimulationCommand : Command
    {
        public Guid SimulationId { get; set; }
    }
}
