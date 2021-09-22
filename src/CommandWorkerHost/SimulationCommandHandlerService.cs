using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MontyHallProblemSimulation.Application.Protos;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Shared.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MontyHallProblemSimulation.Application.Protos.SimulationCommand;

namespace MontyHallProblemSimulation.Application.CommandWorkerHost
{
    public class SimulationCommandHandlerService : SimulationCommandBase
    {
        private readonly ICommandHandlingOrchestrator orchestrator;

        public SimulationCommandHandlerService(ICommandHandlingOrchestrator orchestrator)
        {
            this.orchestrator = orchestrator;
        }

        public override async Task<Empty> ApplyCommand(CommandModel commandModel, ServerCallContext context)
        {
            var type = System.Type.GetType(commandModel.AssemblyName);
            var method = typeof(JsonConvert)
                                 .GetMethod("DeserializeObject",
                                    BindingFlags.Public | BindingFlags.Static,
                                    new ReflectionBinder(),
                                    new[] { typeof(string) },
                                    null)
                                .MakeGenericMethod(type);

            var command = (Command)Convert.ChangeType(method.Invoke(null, new object[] { commandModel.CommandPayload }), type);

            orchestrator.InitiateCommandHandling(command);

            return new Empty();
        }
    }
}
