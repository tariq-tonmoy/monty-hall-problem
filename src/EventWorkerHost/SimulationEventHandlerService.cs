using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MontyHallProblemSimulation.Application.Protos;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Shared.Utility;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using static MontyHallProblemSimulation.Application.Protos.SimulationEvent;

namespace MontyHallProblemSimulation.ReadSide.EventWorkerHost
{
    public class SimulationEventHandlerService : SimulationEventBase
    {
        private readonly IEventHandlingOrchestrator orchestrator;

        public SimulationEventHandlerService(IEventHandlingOrchestrator orchestrator)
        {
            this.orchestrator = orchestrator;
        }

        public override async Task<Empty> ApplyEvent(EventModel eventModel, ServerCallContext context)
        {
            var type = System.Type.GetType(eventModel.AssemblyName);
            var method = typeof(JsonConvert)
                                 .GetMethod("DeserializeObject",
                                    BindingFlags.Public | BindingFlags.Static,
                                    new ReflectionBinder(),
                                    new[] { typeof(string) },
                                    null)
                                .MakeGenericMethod(type);

            var @event = (DomainEvent)Convert.ChangeType(method.Invoke(null, new object[] { eventModel.EventPayload }), type);

            orchestrator.InitiateEventHandling(@event);

            return new Empty();
        }
    }
}
