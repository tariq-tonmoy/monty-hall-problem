using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using System.Collections.Generic;

namespace MontyHallProblemSimulation.Application.Commands
{
    public class DeactivateSimulationsCommand : Command
    {
        public DeactivateSimulationsCommand()
        {
            this.Simulations = new List<DeactivateSimulationDto>();
        }

        public List<DeactivateSimulationDto> Simulations { get; set; }
    }
}
