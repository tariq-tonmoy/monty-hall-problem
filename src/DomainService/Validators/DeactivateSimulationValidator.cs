using FluentValidation;
using MontyHallProblemSimulation.Domain.SimulationEvents.EventMessages;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;

namespace MontyHallProblemSimulation.Domain.DomainService.Validators
{
    public class DeactivateSimulationValidator<TAggregateRoot> : AbstractValidator<DeactivateSimulationDto>
    where TAggregateRoot : AggregateRoot
    {
        public DeactivateSimulationValidator(TAggregateRoot aggregateRoot, Guid sessionId, string actionName, string serviceName)
        {
            this.RuleFor(x => x.SimulationId)
                    .Must(shipId => aggregateRoot.IsMarkedToDelete == true)
                    .WithState(dto => new BusinessRuleViolationEventMessage(
                        EventMessageType.FAILED,
                        SimulationBusinessRuleViolationCodes.SimulationIsAlreadyDeactivated,
                        sessionId,
                        dto.SimulationId,
                        "Simulation already deactivated",
                        nameof(dto.SimulationId),
                        dto.SimulationId,
                        actionName,
                        serviceName));
        }
    }
}
