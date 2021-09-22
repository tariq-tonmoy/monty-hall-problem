using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MontyHallProblemSimulation.Application.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MontyHallProblemSimulation.Application.Protos.SimulationCommand;

namespace MontyHallProblemSimulation.Application.CommandWorkerHost
{
    public class SimulationCommandHandlerService : SimulationCommandBase
    {
        public override async Task<Empty> ApplyCommand(CommandModel commandModel, ServerCallContext context)
        {
            return new Empty();
        }
    }
}
