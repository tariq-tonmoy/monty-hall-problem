using MontyHallProblemSimulation.External.Protos;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Grpc.Comm
{
    public class NotificationEventPublishService : IPublishEventService<NotificationEvent.NotificationEventClient>
    {
        private readonly NotificationEvent.NotificationEventClient client;
        private readonly IReflectionUtilityProvider reflectionUtility;

        public NotificationEventPublishService(NotificationEvent.NotificationEventClient client, IReflectionUtilityProvider reflectionUtility)
        {
            this.client = client;
            this.reflectionUtility = reflectionUtility;
        }

        public async Task PublishMessageAsync<TEvent>(TEvent @event) where TEvent : DomainEvent
        {
            var messagePayload = JsonConvert.SerializeObject(@event);
            await this.client.ApplyEventAsync(new EventModel()
            {
                EventPayload = messagePayload,
                AssemblyName = this.reflectionUtility.GetFullyQualifiedAssemblyName(@event.GetType()),
            });
        }
    }
}
