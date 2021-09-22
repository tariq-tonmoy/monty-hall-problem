using Microsoft.AspNetCore.Mvc;

namespace MontyHallProblemSimulation.Shared.SharedDto.Query
{
    [BindProperties(SupportsGet = true)]
    public class QueryModel
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IncludeCount { get; set; }
    }
}
