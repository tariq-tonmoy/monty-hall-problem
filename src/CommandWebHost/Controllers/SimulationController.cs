using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MontyHallProblemSimulation.Application.Commands;
using MontyHallProblemSimulation.Application.Protos;
using MontyHallProblemSimulation.Infrastructure.Core.Abstractions;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Application.CommandWebHost.Controllers
{
    [AllowAnonymous]
    public class SimulationController : ControllerBase
    {
        private readonly IPublishCommandService<SimulationCommand.SimulationCommandClient> client;

        public SimulationController(IPublishCommandService<SimulationCommand.SimulationCommandClient> client)
        {
            this.client = client;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSimulations([FromBody] CreateSimulationsCommand command)
        {
            await this.client.PublishMessageAsync(command);
            return this.Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> RerunSimulations([FromBody] RerunSimulationsCommand command)
        {
            await this.client.PublishMessageAsync(command);
            return this.Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> DeactivateSimulations([FromBody] DeactivateSimulationsCommand command)
        {
            await this.client.PublishMessageAsync(command);
            return this.Accepted();
        }
    }
}
