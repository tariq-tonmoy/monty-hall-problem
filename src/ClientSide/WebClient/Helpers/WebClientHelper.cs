using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.WebClient.Helpers
{
    public static class WebClientHelper
    {
        public const string VisualStudio = nameof(VisualStudio);
        public const string Docker = nameof(Docker);
        public static Guid SessionId = Guid.NewGuid();
        public static string GetEnvironmentKey(string environment) => environment.Contains(VisualStudio) ? VisualStudio : environment.Contains(Docker) ? Docker : string.Empty;
    }
}
