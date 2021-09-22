using System;

namespace MontyHallProblemSimulation.Infrastructure.Core
{
    public interface IMessage
    {
        Guid CorrelationId { get; set; }

        Guid SessionId { get; set; }
    }
}
