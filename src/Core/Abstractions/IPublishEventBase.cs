using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IPublishEventBase
    {
        Task PublishMessageAsync<TEvent>(TEvent @event)
            where TEvent : DomainEvent;
    }
}
