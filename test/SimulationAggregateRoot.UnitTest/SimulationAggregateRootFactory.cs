using MontyHallProblemSimulation.Shared.SharedDto;
using System;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MontyHallProblemSimulation.Infrastructure.Cqrs.Repository;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using MontyHallProblemSimulation.Domain.DomainService.Abstractions;
using MontyHallProblemSimulation.Domain.DomainService.DataModels;
using AggregateRoot = MontyHallProblemSimulation.Domain.SimulationAggregateRoot;
using System.Diagnostics.CodeAnalysis;

namespace SimulationAggregateRoot.UnitTest
{
    [ExcludeFromCodeCoverage]
    public static class SimulationAggregateRootFactory
    {
        public static DateTime LastUpdatedDate = new DateTime(2021, 9, 23, 9, 14, 0);
        public static DateTime CreatedDate = new DateTime(2021, 9, 23, 8, 10, 0);

        public static Guid SessionId = Guid.Parse("c7ede1e4-b41d-4a17-b6dd-27094937d293");
        public static Guid CorrelationId = Guid.Parse("6f4b7449-60e1-4ac5-937d-49db0d55cb8d");

        public static Guid NewSimulationId = Guid.Parse("c7ede1e4-b41d-4a17-b6dd-27094937d000");
        public static Guid DuplicateSimulationId = Guid.Parse("6f4b7449-60e1-4ac5-937d-49db0d55c000");
        public static Guid CommonSimulationId = Guid.Parse("f0f6d39e-1e31-451f-8344-abe948255000");

        public static AggregateRoot.SimulationAggregateRoot CreateAggregateRootFromEventDto(SimulationEventDto dto, bool isMarkedToDelete)
        {
            AggregateRoot.SimulationAggregateRoot aggregateRoot = new AggregateRoot.SimulationAggregateRoot();

            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.Id)).SetValue(aggregateRoot, dto.SimulationId);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.CreatedBy)).SetValue(aggregateRoot, SessionId);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.CreatedDate)).SetValue(aggregateRoot, CreatedDate);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.DoorChanged)).SetValue(aggregateRoot, dto.ChangeDoor);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.FailCount)).SetValue(aggregateRoot, dto.FailCount);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.IsMarkedToDelete)).SetValue(aggregateRoot, isMarkedToDelete);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.LastUpdatedBy)).SetValue(aggregateRoot, SessionId);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.LastUpdatedDate)).SetValue(aggregateRoot, LastUpdatedDate);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.NumberOfSimulations)).SetValue(aggregateRoot, dto.NumberOfSimulations);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.SuccessCount)).SetValue(aggregateRoot, dto.SuccessCount);
            aggregateRoot.GetType().GetProperty(nameof(AggregateRoot.SimulationAggregateRoot.SuccessRatio)).SetValue(aggregateRoot, dto.SuccessRatio);

            return aggregateRoot;
        }


        public static Mock<IAggregateRootRepository<AggregateRoot.SimulationAggregateRoot>> SetUpMockIAggregateRootRepositoryForExistingAggregateRoot()
        {
            Mock<IAggregateRootRepository<AggregateRoot.SimulationAggregateRoot>> mock = new Mock<IAggregateRootRepository<AggregateRoot.SimulationAggregateRoot>>();
            mock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            return mock;
        }

        public static Mock<IAggregateRootRepository<AggregateRoot.SimulationAggregateRoot>> SetUpMockIAggregateRootRepositoryForNewAggregateRoot()
        {
            Mock<IAggregateRootRepository<AggregateRoot.SimulationAggregateRoot>> mock = new Mock<IAggregateRootRepository<AggregateRoot.SimulationAggregateRoot>>();
            mock.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            return mock;
        }

        public static Mock<IDateTimeProvider> SetUpMockIDateTimeProviderWithCreatedDate()
        {
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.GetUtcDateTime()).Returns(CreatedDate);
            return mock;
        }

        public static Mock<IRandomNumberGeneratorService> SetUpMockIRandomNumberGeneratorService_10_1_1_1()
        {
            Mock<IRandomNumberGeneratorService> mock = new Mock<IRandomNumberGeneratorService>();
            mock.Setup(x => x.GenerateRandomNumbers(It.IsAny<List<RandomNumberGeneratorPayload>>())).Returns(
                    new List<int[]>()
                    {
                        new int[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        new int[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        new int[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    }
                );
            return mock;
        }

        public static Mock<IRandomNumberGeneratorService> SetUpMockIRandomNumberGeneratorService_10_2_1_0()
        {
            Mock<IRandomNumberGeneratorService> mock = new Mock<IRandomNumberGeneratorService>();
            mock.Setup(x => x.GenerateRandomNumbers(It.IsAny<List<RandomNumberGeneratorPayload>>())).Returns(
                    new List<int[]>()
                    {
                        new int[]{ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                        new int[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    }
                );
            return mock;
        }

        public static Mock<IMontyHallProblemSimulationService> SetUpMockIMontyHallProblemSimulationService_10_9()
        {
            Mock<IMontyHallProblemSimulationService> mock = new Mock<IMontyHallProblemSimulationService>();
            mock.Setup(x => x.SimulateMontyHallProblem(It.Is<int>(x => x == 10), It.IsAny<bool>())).Returns(
                    new SimulationResponse()
                    {
                        NumberOfSimulations = 10,
                        SuccessCount = 7,
                        FailCount = 3
                    }
                );
            mock.Setup(x => x.SimulateMontyHallProblem(It.Is<int>(x => x == 9), It.IsAny<bool>())).Returns(
                    new SimulationResponse()
                    {
                        NumberOfSimulations = 9,
                        SuccessCount = 3,
                        FailCount = 6
                    }
                );
            return mock;
        }

        public static Mock<IMontyHallProblemSimulationService> SetUpMockIMontyHallProblemSimulationService_10_9_Alt()
        {
            Mock<IMontyHallProblemSimulationService> mock = new Mock<IMontyHallProblemSimulationService>();
            mock.Setup(x => x.SimulateMontyHallProblem(It.Is<int>(x => x == 10), It.IsAny<bool>())).Returns(
                    new SimulationResponse()
                    {
                        NumberOfSimulations = 10,
                        SuccessCount = 3,
                        FailCount = 7
                    }
                );
            mock.Setup(x => x.SimulateMontyHallProblem(It.Is<int>(x => x == 9), It.IsAny<bool>())).Returns(
                    new SimulationResponse()
                    {
                        NumberOfSimulations = 9,
                        SuccessCount = 6,
                        FailCount = 3
                    }
                );
            return mock;
        }

        public static SimulationEventDto GetValidSimulationEvent_19()
        {
            return new SimulationEventDto()
            {
                ChangeDoor = false,
                FailCount = 9,
                NumberOfSimulations = 19,
                SuccessCount = 10,
                SimulationId = NewSimulationId,
                SuccessRatio = (10.0 / 19.0) * 100.0
            };
        }

        public static SimulationEventDto GetValidSimulationEvent_19_Alt()
        {
            return new SimulationEventDto()
            {
                ChangeDoor = false,
                FailCount = 10,
                NumberOfSimulations = 19,
                SuccessCount = 9,
                SimulationId = NewSimulationId,
                SuccessRatio = (9.0 / 19.0) * 100.0
            };
        }

        public static CreateSimulationDto GetCreateSimulationDtoFromSimulationEventDto(SimulationEventDto dto)
        {
            return new CreateSimulationDto()
            {
                ChangeDoor = dto.ChangeDoor,
                NumberOfSimulations = dto.NumberOfSimulations,
                SimulationId = dto.SimulationId,
            };
        }

    }
}
