using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.DataModels
{
    public class SimulationResponse
    {
        public int NumberOfSimulations { get; set; }

        public int SuccessCount { get; set; }

        public int FailCount { get; set; }
    }
}
