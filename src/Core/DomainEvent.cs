using System;

namespace MontyHallProblemSimulation.Infrastructure.Core
{
    public class DomainEvent : IMessage
    {
        public Guid CorrelationId { get; set; }

        public Guid SessionId { get; set; }
    }
}
