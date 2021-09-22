using FluentValidation.Results;
using MontyHallProblemSimulation.Domain.DomainService.Abstractions;
using MontyHallProblemSimulation.Domain.DomainService.Validators;
using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Domain.SimulationEvents.EventMessages;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Shared.SharedDto;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;
using System.Collections.Generic;
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

        private void RunSimulation(int bacthSize, IMontyHallProblemSimulationService simulationService)
        {
            for (long i = 0; ; i += bacthSize)
            {
                bool lastIteration = false;
                int currentBatchSize = bacthSize;
                if (i + bacthSize > this.NumberOfSimulations)
                {
                    currentBatchSize = (int)(this.NumberOfSimulations - i);
                    if (currentBatchSize <= 0)
                    {
                        break;
                    }

                    lastIteration = true;
                }

                var simulationResult = simulationService.SimulateMontyHallProblem(currentBatchSize, this.DoorChanged);
                this.SuccessCount += simulationResult.SuccessCount;
                this.FailCount += simulationResult.FailCount;

                if (lastIteration == true)
                {
                    break;
                }
            }

            this.SuccessRatio = Math.Round((((double)this.SuccessCount) / ((double)(this.SuccessCount + this.FailCount))) * 100.0, 8);
        }

        private void Reset()
        {
            this.SuccessCount = 0L;
            this.FailCount = 0L;
            this.SuccessRatio = 0.0;
        }

        private SimulationEventDto ConvertAggregateRootToDto()
        {
            return new SimulationEventDto()
            {
                ChangeDoor = this.DoorChanged,
                FailCount = this.FailCount,
                NumberOfSimulations = this.NumberOfSimulations,
                SimulationId = this.Id,
                SuccessCount = this.SuccessCount,
                SuccessRatio = this.SuccessRatio,
            };
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

            this.RunSimulation(batchSize, simulationService);

            SimulationCreatedEvent @event = new SimulationCreatedEvent(simulation, this.ConvertAggregateRootToDto(), sessionId, correlationId);
            this.AddEvent(@event);
        }

        public void RerunSimulation(
            RerunSimulationDto simulation,
            Guid sessionId,
            Guid correlationId,
            int batchSize,
            IDateTimeProvider dateTimeProvider,
            IMontyHallProblemSimulationService simulationService)
        {
            var validationResults = new RerunSimulationValidator<SimulationAggregateRoot>(this, sessionId, nameof(this.RerunSimulation), nameof(SimulationAggregateRoot)).Validate(simulation);
            this.AddValidationEvents(validationResults, simulation.SimulationId, sessionId, correlationId);
            if (this.DomainEvents != null && this.DomainEvents.Any(x => x is BusinessRuleViolatedEvent))
            {
                return;
            }

            this.SetDefaultValues(sessionId, dateTimeProvider);
            this.Reset();

            this.RunSimulation(batchSize, simulationService);

            SimulationRerunEvent @event = new SimulationRerunEvent(simulation, this.ConvertAggregateRootToDto(), sessionId, correlationId);
            this.AddEvent(@event);
        }

        public void DeactivateSimulation(
            DeactivateSimulationDto simulation,
            Guid sessionId,
            Guid correlationId,
            IDateTimeProvider dateTimeProvider)
        {
            var validationResults = new DeactivateSimulationValidator<SimulationAggregateRoot>(this, sessionId, nameof(this.DeactivateSimulation), nameof(SimulationAggregateRoot)).Validate(simulation);
            this.AddValidationEvents(validationResults, simulation.SimulationId, sessionId, correlationId);
            if (this.DomainEvents != null && this.DomainEvents.Any(x => x is BusinessRuleViolatedEvent))
            {
                return;
            }

            this.SetDefaultValues(sessionId, dateTimeProvider);
            this.IsMarkedToDelete = true;

            SimulationDeactivatedEvent @event = new SimulationDeactivatedEvent(simulation, sessionId, correlationId);
            this.AddEvent(@event);
        }

        public void HandleMissingSimulation(
            Guid simulationId,
            Guid sessionId,
            Guid correlationId)
        {
            this.AddEvent(
                new SimulationBusinessRuleViolatedEvent(
                    simulationId,
                    new List<EventMessage>()
                    {
                        new BusinessRuleViolationEventMessage(
                        EventMessageType.FAILED,
                        SimulationBusinessRuleViolationCodes.SimulationDoesNotExists,
                        sessionId,
                        simulationId,
                        "SimulationId doesnot exist",
                        nameof(simulationId),
                        simulationId,
                        nameof(this.HandleMissingSimulation),
                        nameof(SimulationBusinessRuleViolatedEvent))
                    },
                    sessionId,
                    correlationId));
        }
    }
}
