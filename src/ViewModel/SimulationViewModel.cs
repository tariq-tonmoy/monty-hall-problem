using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;

namespace MontyHallProblemSimulation.ReadSide.ViewModel
{
    public class SimulationViewModel : ViewModelBase
    {
        public SimulationViewModel(
            Guid id,
            int version,
            Guid createdBy,
            Guid lastUpdatedBy,
            DateTime createdDate,
            DateTime lastUpdatedDate,
            bool isMarkedToDelete,
            bool doorChanged,
            long successCount,
            long failCount,
            long numberOfSimulations,
            double successRatio)
        {
            this.Id = id;
            this.Version = version;
            this.CreatedBy = createdBy;
            this.LastUpdatedBy = lastUpdatedBy;
            this.CreatedDate = createdDate;
            this.LastUpdatedDate = lastUpdatedDate;
            this.IsMarkedToDelete = isMarkedToDelete;
            this.DoorChanged = doorChanged;
            this.SuccessCount = successCount;
            this.FailCount = failCount;
            this.SuccessRatio = successRatio;
            this.NumberOfSimulations = numberOfSimulations;
        }

        public bool DoorChanged { get; protected set; }

        public long SuccessCount { get; protected set; }

        public long FailCount { get; protected set; }

        public long NumberOfSimulations { get; protected set; }

        public double SuccessRatio { get; protected set; }

        public void UpdateSimulationViewModel(SimulationEventDto simulationEvent, Guid sessionId, IDateTimeProvider dateTimeProvider)
        {
            this.DoorChanged = simulationEvent.ChangeDoor;
            this.FailCount = simulationEvent.FailCount;
            this.LastUpdatedBy = sessionId;
            this.LastUpdatedDate = dateTimeProvider.GetUtcDateTime();
            this.NumberOfSimulations = simulationEvent.NumberOfSimulations;
            this.SuccessCount = simulationEvent.SuccessCount;
            this.SuccessRatio = simulationEvent.SuccessRatio;
            this.Version++;
        }
    }
}
