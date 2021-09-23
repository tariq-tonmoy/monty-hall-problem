using MontyHallProblemSimulation.Domain.DomainService.Service.Imps;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace SimulationAggregateRoot.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class MontyHallProblemSimulationServiceTests
    {
        [Fact]
        public void Should_Have_100_Success_When_All_Picked_Have_Cars_No_Change()
        {
            MontyHallProblemSimulationService service = new MontyHallProblemSimulationService(SimulationAggregateRootFactory.SetUpMockIRandomNumberGeneratorService_10_1_1_1().Object);
            var res = service.SimulateMontyHallProblem(10, false);

            Assert.Equal(10, res.SuccessCount);
            Assert.Equal(0, res.FailCount);
        }

        [Fact]
        public void Should_Have_0_Success_When_All_Picked_Have_Cars_Door_Change()
        {
            MontyHallProblemSimulationService service = new MontyHallProblemSimulationService(SimulationAggregateRootFactory.SetUpMockIRandomNumberGeneratorService_10_1_1_1().Object);
            var res = service.SimulateMontyHallProblem(10, true);

            Assert.Equal(0, res.SuccessCount);
            Assert.Equal(10, res.FailCount);
        }

        [Fact]
        public void Should_Have_100_Success_When_All_Picked_Have_Goats_Door_Change()
        {
            MontyHallProblemSimulationService service = new MontyHallProblemSimulationService(SimulationAggregateRootFactory.SetUpMockIRandomNumberGeneratorService_10_2_1_0().Object);
            var res = service.SimulateMontyHallProblem(10, true);

            Assert.Equal(10, res.SuccessCount);
            Assert.Equal(0, res.FailCount);
        }

        [Fact]
        public void Should_Have_0_Success_When_All_Picked_Have_Goats_No_Change()
        {
            MontyHallProblemSimulationService service = new MontyHallProblemSimulationService(SimulationAggregateRootFactory.SetUpMockIRandomNumberGeneratorService_10_2_1_0().Object);
            var res = service.SimulateMontyHallProblem(10, false);

            Assert.Equal(0, res.SuccessCount);
            Assert.Equal(10, res.FailCount);
        }
    }
}
