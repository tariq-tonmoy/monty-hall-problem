using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using System.Collections.Generic;

namespace MontyHallProblemSimulation.Application.Commands
{
    public class CreateSimulationsCommand : Command
    {
        public CreateSimulationsCommand()
        {
            this.Simulations = new List<CreateSimulationDto>();
        }

        public List<CreateSimulationDto> Simulations { get; set; }
    }
}
