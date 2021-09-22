using MontyHallProblemSimulation.ReadSide.QueryWebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ReadSide.QueryWebHost.Abstractions
{
    public interface IQueryRepository
    {
        QueryResponseWithCount GetSimulationResults(QueryModel query);
    }
}
