using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ReadSide.QueryWebHost.Models
{
    public class QueryResponseWithCount
    {
        public IEnumerable<QueryResponse> Responses { get; set; }

        public long Count { get; set; }
    }
}
