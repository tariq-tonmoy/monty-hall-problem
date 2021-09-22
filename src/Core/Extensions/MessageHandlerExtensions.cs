using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using MontyHallProblemSimulation.Infrastructure.Core.Service.Imps;

namespace SimulationService.Infrastructure.Core.Extensions
{
    public static class MessageHandlerExtensions
    {
        public static IMessageHandlerBuilder AddMessageHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandlingOrchestrator, CommandHandlingOrchestrator>();
            services.AddTransient<IEventHandlingOrchestrator, EventHandlingOrchestrator>();
            IMessageHandlerBuilder builder = new MessageHandlerBuilder(services);

            return builder;
        }
    }
}
