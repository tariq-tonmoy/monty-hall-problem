using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;

namespace MontyHallProblemSimulation.Shared.Utility.Extensions
{
    public static class UtilityExtensions
    {
        public static IServiceCollection AddUtilities(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IReflectionUtilityProvider, ReflectionUtilityProvider>();
            JsonConvert.DefaultSettings = () => new JsonSerializationSessingsProvider().GetJsonSerializerSettingsForPrivateProperties();
            return services;
        }
    }
}
