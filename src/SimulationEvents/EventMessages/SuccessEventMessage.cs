using MontyHallProblemSimulation.Infrastructure.Core;

namespace MontyHallProblemSimulation.Domain.SimulationEvents.EventMessages
{
    public class SuccessEventMessage : EventMessage
    {
        public SuccessEventMessage(EventMessageType eventMessageCode, string code)
            : base(eventMessageCode, code, new object[] { })
        {
        }
    }
}
