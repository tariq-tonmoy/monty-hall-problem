using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Core.Service.Imps
{
    public class PublishEventAsBulkOnGrpcService : IPublishEventsAsBulkService
    {
        private readonly IEnumerable<IPublishEventBase> eventClients;

        public PublishEventAsBulkOnGrpcService(IEnumerable<IPublishEventBase> eventClients)
        {
            this.eventClients = eventClients;
        }

        public async Task PublishBulkEvents<TEvent>(IEnumerable<TEvent> events) where TEvent : DomainEvent
        {
            if (events != null && events.Any())
            {
                foreach (var @event in events)
                {
                    if (this.eventClients != null && this.eventClients.Any())
                    {
                        foreach (var eventClient in this.eventClients)
                        {
                            await eventClient.PublishMessageAsync(@event);
                        }
                    }
                }
            }
        }
    }
}
