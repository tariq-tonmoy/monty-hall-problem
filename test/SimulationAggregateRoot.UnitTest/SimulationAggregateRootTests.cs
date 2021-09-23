using MontyHallProblemSimulation.Domain.SimulationEvents;
using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using AggregateRoot = MontyHallProblemSimulation.Domain.SimulationAggregateRoot;

namespace SimulationAggregateRoot.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class SimulationAggregateRootTests
    {
        [Fact]
        public void Should_Raise_Simulation_Created_Event_With_Valid_Data()
        {
            AggregateRoot.SimulationAggregateRoot aggregateRoot = new AggregateRoot.SimulationAggregateRoot();
            var validEventDto = SimulationAggregateRootFactory.GetValidSimulationEvent_19();
            var createSimulationDto = SimulationAggregateRootFactory.GetCreateSimulationDtoFromSimulationEventDto(validEventDto);

            aggregateRoot.CreateSimulation(
                createSimulationDto,
                SimulationAggregateRootFactory.SessionId,
                SimulationAggregateRootFactory.CorrelationId,
                10,
                SimulationAggregateRootFactory.SetUpMockIDateTimeProviderWithCreatedDate().Object,
                SimulationAggregateRootFactory.SetUpMockIAggregateRootRepositoryForNewAggregateRoot().Object,
                SimulationAggregateRootFactory.SetUpMockIMontyHallProblemSimulationService_10_9().Object);

            Assert.Contains(aggregateRoot.DomainEvents,
                @event => @event is SimulationCreatedEvent simulationCreatedEvent
                       && simulationCreatedEvent.Simulation.FailCount == validEventDto.FailCount
                       && simulationCreatedEvent.Simulation.SuccessCount == validEventDto.SuccessCount
                       && simulationCreatedEvent.Simulation.SuccessRatio == Math.Round(validEventDto.SuccessRatio, 8));
        }

        [Fact]
        public void Should_Raise_Simulation_Rerun_Event_With_Valid_Data()
        {
            var validEventDto = SimulationAggregateRootFactory.GetValidSimulationEvent_19();
            var validEventDtoAlt = SimulationAggregateRootFactory.GetValidSimulationEvent_19_Alt();

            AggregateRoot.SimulationAggregateRoot aggregateRoot = SimulationAggregateRootFactory.CreateAggregateRootFromEventDto(validEventDto, false);

            var updateSimulationDto = new RerunSimulationDto()
            {
                SimulationId = validEventDto.SimulationId,
            };

            aggregateRoot.RerunSimulation(
                updateSimulationDto,
                SimulationAggregateRootFactory.SessionId,
                SimulationAggregateRootFactory.CorrelationId,
                10,
                SimulationAggregateRootFactory.SetUpMockIDateTimeProviderWithCreatedDate().Object,
                SimulationAggregateRootFactory.SetUpMockIMontyHallProblemSimulationService_10_9_Alt().Object);

            Assert.Contains(aggregateRoot.DomainEvents,
                @event => @event is SimulationRerunEvent simulationRerunEvent
                       && simulationRerunEvent.Simulation.FailCount == validEventDtoAlt.FailCount
                       && simulationRerunEvent.Simulation.SuccessCount == validEventDtoAlt.SuccessCount
                       && simulationRerunEvent.Simulation.SuccessRatio == Math.Round(validEventDtoAlt.SuccessRatio, 8));
        }
    }
}
