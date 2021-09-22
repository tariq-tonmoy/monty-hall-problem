using FluentValidation;
using MontyHallProblemSimulation.Domain.SimulationEvents.EventMessages;
using MontyHallProblemSimulation.Infrastructure.Core;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.Validators
{
    public class CreateSimulationValidator<TAggregateRoot> : AbstractValidator<CreateSimulationDto>
        where TAggregateRoot : AggregateRoot
    {
        private readonly IAggregateRootRepository<TAggregateRoot> aggregateRootRepository;

        public CreateSimulationValidator(IAggregateRootRepository<TAggregateRoot> aggregateRootRepository, Guid sessionId, string actionName, string serviceName)
        {
            this.aggregateRootRepository = aggregateRootRepository;

            this.RuleFor(x => x.SimulationId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithState(dto => new BusinessRuleViolationEventMessage(
                    EventMessageType.FAILED,
                    SimulationBusinessRuleViolationCodes.PropertyValueMissing,
                    sessionId,
                    Guid.Empty,
                    "SimulationId missing",
                    nameof(dto.SimulationId),
                    Guid.Empty,
                    actionName,
                    serviceName))
                .NotEqual(Guid.Empty)
                .WithState(dto => new BusinessRuleViolationEventMessage(
                    EventMessageType.FAILED,
                    SimulationBusinessRuleViolationCodes.PropertyValueMissing,
                    sessionId,
                    dto.SimulationId,
                    "SimulationId missing",
                    nameof(dto.SimulationId),
                    Guid.Empty,
                    actionName,
                    serviceName))
                .MustAsync(this.IsNewAggregateRoot)
                .WithState(dto => new BusinessRuleViolationEventMessage(
                    EventMessageType.FAILED,
                    SimulationBusinessRuleViolationCodes.SimulationAlreadyCreated,
                    sessionId,
                    dto.SimulationId,
                    "SimulationId already exists",
                    nameof(dto.SimulationId),
                    dto.SimulationId,
                    actionName,
                    serviceName))
                .DependentRules(() =>
                {
                    this.RuleFor(x => x.NumberOfSimulations)
                        .GreaterThan(0)
                        .WithState(dto => new BusinessRuleViolationEventMessage(
                            EventMessageType.FAILED,
                            SimulationBusinessRuleViolationCodes.PropertyValueMissing,
                            sessionId,
                            dto.SimulationId,
                            "NumberOfSimulations missing",
                            nameof(dto.NumberOfSimulations),
                            dto.NumberOfSimulations,
                            actionName,
                            serviceName));
                });
        }

        private async Task<bool> IsNewAggregateRoot(Guid simulationId, CancellationToken token)
        {
            return (await this.aggregateRootRepository.ExistsAsync(simulationId)) == false;
        }
    }
}
