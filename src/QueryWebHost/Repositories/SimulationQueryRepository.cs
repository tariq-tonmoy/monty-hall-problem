using Microsoft.EntityFrameworkCore;
using MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite;
using MontyHallProblemSimulation.ReadSide.QueryWebHost.Abstractions;
using MontyHallProblemSimulation.ReadSide.ViewModel;
using MontyHallProblemSimulation.Shared.SharedDto.Query;
using System.Linq;

namespace MontyHallProblemSimulation.ReadSide.QueryWebHost.Repositories
{
    public class SimulationQueryRepository : IQueryRepository
    {
        private readonly SimulationViewModelDbContext dbContext;

        public SimulationQueryRepository(SimulationViewModelDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.Database.Migrate();
        }

        public QueryResponseWithCount GetSimulationResults(QueryModel query)
        {
            QueryResponseWithCount response = new QueryResponseWithCount()
            {
                Count = 0,
            };

            if (query.IncludeCount)
            {
                response.Count = this.dbContext.Set<SimulationViewModel>().Count();
            }

            response.Responses = this.dbContext.Set<SimulationViewModel>()
                                     .OrderByDescending(x => x.LastUpdatedDate)
                                     .Skip(query.PageIndex * query.PageSize)
                                     .Take(query.PageSize)
                                     .Select(x => new QueryResponse()
                                     {
                                         ChangeDoor = x.DoorChanged,
                                         FailCount = x.FailCount,
                                         LastUpdateDate = x.LastUpdatedDate,
                                         NumberOfSimulations = x.NumberOfSimulations,
                                         SimulationId = x.Id,
                                         SuccessCount = x.SuccessCount,
                                         SuccessRatio = x.SuccessRatio
                                     });

            return response;
        }
    }
}
