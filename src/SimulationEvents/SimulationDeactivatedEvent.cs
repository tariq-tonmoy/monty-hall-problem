using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;

namespace MontyHallProblemSimulation.Domain.SimulationEvents
{
    public class SimulationDeactivatedEvent : DomainEvent
    {
        public SimulationDeactivatedEvent(DeactivateSimulationDto simulationDto, Guid sessionId, Guid correlationId)
        {
            this.SimulationDto = simulationDto;
            this.SessionId = sessionId;
            this.CorrelationId = correlationId;
        }

        public DeactivateSimulationDto SimulationDto { get; }
    }
}
