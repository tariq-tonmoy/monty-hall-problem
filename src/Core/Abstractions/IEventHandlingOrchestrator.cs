namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IEventHandlingOrchestrator
    {
        delegate void EventReceivedDelegate<in TEvent>(TEvent @event)
            where TEvent : DomainEvent;

        event EventReceivedDelegate<DomainEvent> DomainEventReceivedEvent;

        void InitiateEventHandling<TEvent>(TEvent @event)
            where TEvent : DomainEvent;
    }
}
