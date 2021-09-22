using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ReadSide.QueryWebHost.Models
{
    public class QueryResponse
    {
        public DateTime LastUpdateDate { get; set; }

        public Guid SimulationId { get; set; }

        public long NumberOfSimulations { get; set; }

        public bool ChangeDoor { get; set; }

        public long SuccessCount { get; set; }

        public long FailCount { get; set; }

        public double SuccessRatio { get; set; }
    }
}
