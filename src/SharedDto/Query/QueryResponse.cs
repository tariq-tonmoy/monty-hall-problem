using System;

namespace MontyHallProblemSimulation.Shared.SharedDto.Query
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
