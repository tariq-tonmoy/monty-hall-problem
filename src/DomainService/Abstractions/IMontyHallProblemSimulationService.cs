using MontyHallProblemSimulation.Domain.DomainService.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.Abstractions
{
    public interface IMontyHallProblemSimulationService
    {
        SimulationResponse SimulateMontyHallProblem(int numberOfSimulations, bool changeDoor);
    }
}
