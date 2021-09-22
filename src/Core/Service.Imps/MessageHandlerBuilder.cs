using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MontyHallProblemSimulation.Infrastructure.Core.Service.Imps
{
    public class MessageHandlerBuilder : IMessageHandlerBuilder
    {
        private readonly IServiceCollection services;

        public MessageHandlerBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public IMessageHandlerBuilder AddCommandHandler<TCommand>(Type commandHandlerType)
            where TCommand : Command
        {
            this.services.AddScoped(typeof(IAsyncCommandHandler<TCommand>), commandHandlerType);
            return this;
        }

        public IMessageHandlerBuilder AddCommandHandler(Type interfaceType, Type commandHandlerType)
        {
            this.services.AddScoped(interfaceType, commandHandlerType);
            return this;
        }

        public IMessageHandlerBuilder AddEventHandler<TEvent>(Type eventHandlerType)
            where TEvent : DomainEvent
        {
            this.services.AddScoped(typeof(IAsyncEventHandler<TEvent>), eventHandlerType);
            return this;
        }

        public IMessageHandlerBuilder AddEventHandler(Type interfaceType, Type eventHandlerType)
        {
            this.services.AddScoped(interfaceType, eventHandlerType);
            return this;
        }
    }
}
