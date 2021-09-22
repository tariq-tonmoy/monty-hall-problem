using MontyHallProblemSimulation.Shared.SharedDto.Query;

namespace MontyHallProblemSimulation.ReadSide.QueryWebHost.Abstractions
{
    public interface IQueryRepository
    {
        QueryResponseWithCount GetSimulationResults(QueryModel query);
    }
}
