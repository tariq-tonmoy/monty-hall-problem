using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;

namespace MontyHallProblemSimulation.Infrastructure.Core.Service.Imps
{
    public abstract class MessageHandlingOrchestratorHelper
    {
        private const string HandlerMethodName = "HandleAsync";

        protected async Task HandleCommandReceivedEventAsync<TMessage>(TMessage message, IServiceScope scope)
            where TMessage : IMessage
        {
            if (message.SessionId == System.Guid.Empty)
            {
                throw new Exception("SessionId is empty");
            }

            var messageType = message.GetType();
            var handlerType = message is Command ? typeof(IAsyncCommandHandler<>).MakeGenericType(messageType) : typeof(IAsyncEventHandler<>).MakeGenericType(messageType);
            var handler = scope.ServiceProvider.GetService(handlerType);
            if (handler != null)
            {
                var response = (Task)handler.GetType().GetMethod(HandlerMethodName)?.Invoke(handler, new object[] { message });
                if (response != null)
                {
                    await response;
                }
            }
        }
    }
}
