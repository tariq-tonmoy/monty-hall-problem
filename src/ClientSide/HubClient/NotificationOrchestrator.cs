using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.HubClient
{
    public class NotificationOrchestrator
    {
        public delegate void EnvironmentChangedDelegate(string environment);
        public event EnvironmentChangedDelegate OnEnvironmentChanged;

        public delegate void ActionPerformedDelegate(ActionType actionType, SimulationEventDto simulation = null);
        public event ActionPerformedDelegate OnActionPerformed;

        public delegate void AppInitializedDelegate();
        public event AppInitializedDelegate OnAppInitialized;

        public NotificationOrchestrator()
        {

        }

        public void EnvironmentChanged(string environment)
        {
            this.OnEnvironmentChanged?.Invoke(environment);
        }

        public void ActionPerformed(ActionType actionType, SimulationEventDto simulation = null)
        {
            this.OnActionPerformed?.Invoke(actionType, simulation);
        }

        public void AppInitialized()
        {
            this.OnAppInitialized?.Invoke();
        }
    }
}
