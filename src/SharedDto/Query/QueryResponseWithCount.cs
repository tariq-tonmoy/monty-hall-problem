using System.Collections.Generic;

namespace MontyHallProblemSimulation.Shared.SharedDto.Query
{
    public class QueryResponseWithCount
    {
        public IEnumerable<QueryResponse> Responses { get; set; }

        public long Count { get; set; }
    }
}
