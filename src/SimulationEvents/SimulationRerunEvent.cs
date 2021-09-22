using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;

namespace MontyHallProblemSimulation.Domain.SimulationEvents
{
    public class SimulationRerunEvent : DomainEvent
    {
        public SimulationRerunEvent(RerunSimulationDto simulationDto, SimulationEventDto simulation, Guid sessionId, Guid correlationId)
        {
            this.SimulationDto = simulationDto;
            this.Simulation = simulation;
            this.SessionId = sessionId;
            this.CorrelationId = correlationId;
        }

        public RerunSimulationDto SimulationDto { get; }

        public SimulationEventDto Simulation { get; }
    }
}
