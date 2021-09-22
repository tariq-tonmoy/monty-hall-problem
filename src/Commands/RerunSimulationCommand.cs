using MontyHallProblemSimulation.Infrastructure.Core;
using System;

namespace MontyHallProblemSimulation.Application.Commands
{
    public class RerunSimulationCommand : Command
    {
        public Guid SimulationId { get; set; }
    }
}
