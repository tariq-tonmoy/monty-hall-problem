using Microsoft.Extensions.Options;
using MontyHallProblemSimulation.ClientSide.WebClient.Abstractions;
using MontyHallProblemSimulation.ClientSide.WebClient.Helpers;
using MontyHallProblemSimulation.Shared.SharedDto.ConfigurationModels;
using MontyHallProblemSimulation.Shared.SharedDto.Query;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.WebClient
{
    public class QueryClientService : IQueryClient
    {
        private readonly IOptions<AppSettings> appsettings;
        private readonly IHttpHelper httpHelper;

        public QueryClientService(IOptions<AppSettings> appsettings, IHttpHelper httpHelper)
        {
            this.appsettings = appsettings;
            this.httpHelper = httpHelper;
        }

        public async Task<QueryResponseWithCount> GetSimulations(int pageIndex, int pageSize, string environment)
        {
            string env = WebClientHelper.GetEnvironmentKey(environment);

            if (!string.IsNullOrWhiteSpace(env))
            {
                var queryBaseUrl = this.appsettings.Value.ConnectionUrls.First(x => x.Environment == env)?.QueryWebHost;
                var queryUrl = $"{queryBaseUrl}/SimulationQuery/GetSimulations?PageIndex={pageIndex}&PageSize={pageSize}&IncludeCount=true";
                var response = await this.httpHelper.SendGetMethodAsync(queryUrl);

                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<QueryResponseWithCount>(response.Payload);
                }
            }

            return new QueryResponseWithCount();
        }
    }
}
