using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.HubClient
{
    public static class NotificationPayloadHelper
    {
        public static TEvent GetDomainEventFromNotificationPayload<TEvent>(string payload, string assemblyName)
            where TEvent : DomainEvent
        {
            var type = System.Type.GetType(assemblyName);
            var method = typeof(JsonConvert)
                                 .GetMethod("DeserializeObject",
                                    BindingFlags.Public | BindingFlags.Static,
                                    new ReflectionBinder(),
                                    new[] { typeof(string) },
                                    null)
                                .MakeGenericMethod(type);

            var @event = (TEvent)Convert.ChangeType(method.Invoke(null, new object[] { payload }), type);

            return @event;
        }
    }
}
