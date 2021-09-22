using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IAsyncEventHandler<in TEvent>
        where TEvent : DomainEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
