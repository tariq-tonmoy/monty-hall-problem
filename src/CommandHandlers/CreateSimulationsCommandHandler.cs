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
    public class CreateSimulationsCommandHandler : IAsyncCommandHandler<CreateSimulationsCommand>
    {
        public Task<CommandRespose> HandleAsync(CreateSimulationsCommand command)
        {
            throw new NotImplementedException();
        }
    }
}