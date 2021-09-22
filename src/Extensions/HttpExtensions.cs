using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Infrastructure.Extensions
{
    public static class HttpExtensions
    {
        public static IServiceCollection AddHttpComponents(this IServiceCollection services)
        {
            services.AddMvcCore((options) =>
            {
            })
            .AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddNewtonsoftJson()
            .AddCors();

            services.AddHealthChecks();

            return services;
        }

        public static IApplicationBuilder UseHttpPipeline(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseRouting();

            applicationBuilder.UseCors((corsPolicyBuilder) =>
                   corsPolicyBuilder
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((string origin) => true)
                   .AllowCredentials()
                   .SetPreflightMaxAge(TimeSpan.FromDays(365)));

            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();

            applicationBuilder.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");

                endpoints.MapHealthChecks("/health");
            });

            return applicationBuilder;
        }
    }
}
