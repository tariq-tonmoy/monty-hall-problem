using FluentValidation;
using MontyHallProblemSimulation.Domain.SimulationEvents.EventMessages;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.Validators
{
    public class RerunSimulationValidator<TAggregateRoot> : AbstractValidator<RerunSimulationDto>
        where TAggregateRoot : AggregateRoot
    {
        public RerunSimulationValidator(TAggregateRoot aggregateRoot, Guid sessionId, string actionName, string serviceName)
        {
            this.RuleFor(x => x.SimulationId)
                .Must(shipId => aggregateRoot.IsMarkedToDelete == false)
                .WithState(dto => new BusinessRuleViolationEventMessage(
                    EventMessageType.FAILED,
                    SimulationBusinessRuleViolationCodes.SimulationDoesNotExists,
                    sessionId,
                    dto.SimulationId,
                    "SimulationId doesnot exist",
                    nameof(dto.SimulationId),
                    dto.SimulationId,
                    actionName,
                    serviceName));
        }
    }
}
