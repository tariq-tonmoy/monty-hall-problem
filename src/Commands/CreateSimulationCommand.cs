using MontyHallProblemSimulation.Infrastructure.Core;
using System;

namespace MontyHallProblemSimulation.Application.Commands
{
    public class CreateSimulationCommand : Command
    {
        public Guid SimulationId { get; set; }

        public long NumberOfSimulations { get; set; }

        public bool ChangeDoor { get; set; }
    }
}
