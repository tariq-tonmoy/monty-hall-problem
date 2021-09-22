using System.Collections.Generic;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IPublishEventsAsBulkService
    {
        Task PublishBulkEvents<TEvent>(IEnumerable<TEvent> events)
            where TEvent : DomainEvent;
    }
}
