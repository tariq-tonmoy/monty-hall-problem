using MontyHallProblemSimulation.ClientSide.WebClient.Abstractions;
using MontyHallProblemSimulation.ClientSide.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.WebClient.Helpers
{
    public class HttpHelper : IHttpHelper
    {
        private readonly IHttpClientFactory clientFactory;

        public HttpHelper(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<HttpHelperResponse> SendGetMethodAsync(string url)
        {
            using (var client = this.clientFactory.CreateClient())
            {
                HttpHelperResponse resp = new HttpHelperResponse();

                var response = await client.GetAsync(url);

                resp.IsSuccessful = response.IsSuccessStatusCode;
                resp.Payload = await response.Content.ReadAsStringAsync();

                return resp;
            }
        }

        public async Task<HttpHelperResponse> SendPostMethodAsync(string url, string stringifiedJson)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url))
            {
                httpRequest.Content = new StringContent(stringifiedJson, Encoding.UTF8, "application/json");
                using (var client = this.clientFactory.CreateClient())
                {
                    HttpHelperResponse resp = new HttpHelperResponse();

                    var response = await client.SendAsync(httpRequest);

                    resp.IsSuccessful = response.IsSuccessStatusCode;
                    resp.Payload = await response.Content.ReadAsStringAsync();

                    return resp;
                }
            }
        }
    }
}
