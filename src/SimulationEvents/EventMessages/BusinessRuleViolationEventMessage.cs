using MontyHallProblemSimulation.Infrastructure.Core;
using System;

namespace MontyHallProblemSimulation.Domain.SimulationEvents.EventMessages
{
    public class BusinessRuleViolationEventMessage : EventMessage
    {
        public BusinessRuleViolationEventMessage(EventMessageType eventMessageCode, string code, Guid sessionId, Guid simulationId, string message, string propertyName, object propertyValue, string actionName, string serviceName)
            : base(eventMessageCode, code, new object[] { sessionId, simulationId, message, propertyName, propertyValue, actionName, serviceName })
        {
        }
    }
}
