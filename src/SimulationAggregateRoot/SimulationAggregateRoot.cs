using FluentValidation.Results;
using MontyHallProblemSimulation.Domain.DomainService.Abstractions;
using MontyHallProblemSimulation.Domain.DomainService.Validators;
using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Shared.SharedDto;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;
using System.Linq;

namespace MontyHallProblemSimulation.Domain.SimulationAggregateRoot
{
    public class SimulationAggregateRoot : AggregateRoot
    {
        public bool DoorChanged { get; protected set; }

        public long SuccessCount { get; protected set; }

        public long FailCount { get; protected set; }

        public long NumberOfSimulations { get; protected set; }

        public double SuccessRatio { get; protected set; }

        private void AddValidationEvents(ValidationResult validationResult, Guid entityId, Guid sessionId, Guid correlationId)
        {
            var errors = validationResult.Errors.Select(e => e.CustomState as EventMessage).ToList();
            if (errors != null && errors.Any())
            {
                this.AddEvent(new SimulationBusinessRuleViolatedEvent(entityId, errors, sessionId, correlationId));
                return;
            }
        }

        public void RunSimulation()
        {

        }

        public void Reset()
        {
            this.SuccessCount = 0L;
            this.FailCount = 0L;
            this.SuccessRatio = 0.0;
        }

        public void CreateSimulation(
            CreateSimulationDto simulation,
            Guid sessionId,
            Guid correlationId,
            int batchSize,
            IDateTimeProvider dateTimeProvider,
            IAggregateRootRepository<SimulationAggregateRoot> aggregateRootRepository,
            IMontyHallProblemSimulationService simulationService)
        {
            var validationResults = new CreateSimulationValidator<SimulationAggregateRoot>(aggregateRootRepository, sessionId, nameof(this.CreateSimulation), nameof(SimulationAggregateRoot)).Validate(simulation);
            this.AddValidationEvents(validationResults, simulation.SimulationId, sessionId, correlationId);
            if (this.DomainEvents != null && this.DomainEvents.Any(x => x is BusinessRuleViolatedEvent))
            {
                return;
            }

            this.Id = simulation.SimulationId;
            this.IsMarkedToDelete = false;
            this.SetDefaultValues(sessionId, dateTimeProvider);
            this.Reset();
            this.NumberOfSimulations = simulation.NumberOfSimulations;
            this.DoorChanged = simulation.ChangeDoor;

            this.RunSimulation();
        }
    }
}
