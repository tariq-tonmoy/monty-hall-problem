using MontyHallProblemSimulation.Application.Commands;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Application.CommandHandlers
{
    public class RerunSimulationsCommandHandler : IAsyncCommandHandler<RerunSimulationsCommand>
    {
        public Task<CommandRespose> HandleAsync(RerunSimulationsCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
