using Microsoft.Extensions.DependencyInjection;
using MontyHallProblemSimulation.Domain.DomainService.Abstractions;
using MontyHallProblemSimulation.Domain.DomainService.Service.Imps;

namespace MontyHallProblemSimulation.Domain.DomainService.Extensions
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddSimulationDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRandomNumberGeneratorService, RandomNumberGeneratorService>();
            serviceCollection.AddSingleton<IMontyHallProblemSimulationService, MontyHallProblemSimulationService>();

            return serviceCollection;
        }
    }
}
