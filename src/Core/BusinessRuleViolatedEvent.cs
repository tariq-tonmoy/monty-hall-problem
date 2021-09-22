using System;
using System.Collections.Generic;

namespace MontyHallProblemSimulation.Infrastructure.Core
{
    public class BusinessRuleViolatedEvent : DomainEvent
    {
        public BusinessRuleViolatedEvent(Guid entityId, IEnumerable<EventMessage> eventMessages, Guid sessionId, Guid correlationId)
        {
            this.EntityId = entityId;
            this.EventMessages = eventMessages;
            this.CorrelationId = correlationId;
            this.SessionId = sessionId;
        }

        public Guid EntityId { get; }

        public IEnumerable<EventMessage> EventMessages { get; }
    }
}
