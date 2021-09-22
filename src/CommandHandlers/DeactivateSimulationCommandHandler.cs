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
    public class DeactivateSimulationCommandHandler : IAsyncCommandHandler<DeactivateSimulationCommand>
    {
        public Task<CommandRespose> HandleAsync(DeactivateSimulationCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
