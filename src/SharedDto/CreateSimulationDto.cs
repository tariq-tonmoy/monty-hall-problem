using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Shared.SharedDto
{
    public class CreateSimulationDto
    {
        public Guid SimulationId { get; set; }

        public long NumberOfSimulations { get; set; }

        public bool ChangeDoor { get; set; }
    }
}
