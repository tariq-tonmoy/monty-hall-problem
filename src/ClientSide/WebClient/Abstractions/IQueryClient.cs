using MontyHallProblemSimulation.Shared.SharedDto.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.WebClient.Abstractions
{
    public interface IQueryClient
    {
        Task<QueryResponseWithCount> GetSimulations(int pageIndex, int pageSize, string environment);
    }
}
