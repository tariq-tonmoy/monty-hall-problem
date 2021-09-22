using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using MontyHallProblemSimulation.External.NotificationHost.Hubs;
using MontyHallProblemSimulation.External.Protos;
using System.Threading.Tasks;
using static MontyHallProblemSimulation.External.Protos.NotificationEvent;

namespace MontyHallProblemSimulation.External.NotificationHost
{
    public class NotificationEventHandlerService : NotificationEventBase
    {
        private readonly IHubContext<NotificationHub> hubContext;

        public NotificationEventHandlerService(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public override async Task<Empty> ApplyEvent(EventModel eventModel, ServerCallContext context)
        {
            await this.hubContext.Clients.All.SendAsync("ReceiveEvent", eventModel.EventPayload, eventModel.AssemblyName);
            return new Empty();
        }
    }
}
