﻿using MontyHallProblemSimulation.Application.Commands;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Application.CommandHandlers
{
    public class CreateSimulationCommandHandler : IAsyncCommandHandler<CreateSimulationCommand>
    {
        public Task<CommandRespose> HandleAsync(CreateSimulationCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
