using System;

namespace MontyHallProblemSimulation.Infrastructure.Core.Abstractions
{
    public interface IMessageHandlerBuilder
    {
        IMessageHandlerBuilder AddCommandHandler<TCommand>(Type commandHandlerType)
            where TCommand : Command;

        IMessageHandlerBuilder AddCommandHandler(Type interfaceType, Type commandHandlerType);

        IMessageHandlerBuilder AddEventHandler<TEvent>(Type eventHandlerType)
            where TEvent : DomainEvent;

        IMessageHandlerBuilder AddEventHandler(Type interfaceType, Type eventHandlerType);
    }
}
