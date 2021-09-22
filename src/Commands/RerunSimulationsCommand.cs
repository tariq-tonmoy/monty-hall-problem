using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using System.Collections.Generic;

namespace MontyHallProblemSimulation.Application.Commands
{
    public class RerunSimulationsCommand : Command
    {
        public RerunSimulationsCommand()
        {
            this.Simulations = new List<RerunSimulationDto>();
        }

        public List<RerunSimulationDto> Simulations { get; set; }
    }
}
