using MontyHallProblemSimulation.ClientSide.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.WebClient.Abstractions
{
    public interface IHttpHelper
    {
        Task<HttpHelperResponse> SendGetMethodAsync(string url);

        Task<HttpHelperResponse> SendPostMethodAsync(string url, string stringifiedJson);
    }
}
