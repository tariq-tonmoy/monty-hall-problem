using MontyHallProblemSimulation.Infrastructure.Core;
using System;
using System.Collections.Generic;

namespace MontyHallProblemSimulation.Domain.SimulationEvents
{
    public class SimulationBusinessRuleViolatedEvent : BusinessRuleViolatedEvent
    {
        public SimulationBusinessRuleViolatedEvent(Guid entityId, IEnumerable<EventMessage> eventMessages, Guid sessionId, Guid correlationId)
            : base(entityId, eventMessages, sessionId, correlationId)
        {
            this.CorrelationId = correlationId;
            this.SessionId = sessionId;
        }
    }
}
