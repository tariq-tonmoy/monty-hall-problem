using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;

namespace MontyHallProblemSimulation.Domain.SimulationEvents
{
    public class SimulationCreatedEvent : DomainEvent
    {
        public SimulationCreatedEvent(CreateSimulationDto simulationDto, SimulationEventDto simulation, Guid sessionId, Guid correlationId)
        {
            this.SimulationDto = simulationDto;
            this.Simulation = simulation;
            this.SessionId = sessionId;
            this.CorrelationId = correlationId;
        }

        public CreateSimulationDto SimulationDto { get; }

        public SimulationEventDto Simulation { get; }
    }
}
