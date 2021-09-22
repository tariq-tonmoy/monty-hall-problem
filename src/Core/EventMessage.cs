using System.Collections;
using System.Collections.Generic;

namespace MontyHallProblemSimulation.Infrastructure.Core
{
    public class EventMessage
    {
        public EventMessage(EventMessageType eventMessageCode, string code, IEnumerable<object> data)
        {
            this.EventMessageCode = eventMessageCode;
            this.Code = code;
            this.Data = data;
        }

        public EventMessageType EventMessageCode { get; }

        public string Code { get; }
        public IEnumerable<object> Data { get; }
    }
}
