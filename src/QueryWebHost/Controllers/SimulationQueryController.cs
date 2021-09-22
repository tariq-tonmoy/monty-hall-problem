using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MontyHallProblemSimulation.ReadSide.QueryWebHost.Abstractions;
using MontyHallProblemSimulation.Shared.SharedDto.Query;

namespace MontyHallProblemSimulation.ReadSide.QueryWebHost.Controllers
{
    [AllowAnonymous]
    public class SimulationQueryController : ControllerBase
    {
        private readonly IQueryRepository repository;

        public SimulationQueryController(IQueryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetSimulations([FromQuery] QueryModel query)
        {
            return this.Ok(this.repository.GetSimulationResults(query));
        }
    }
}
