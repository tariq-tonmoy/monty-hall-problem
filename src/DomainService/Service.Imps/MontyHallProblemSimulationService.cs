using MontyHallProblemSimulation.Domain.DomainService.Abstractions;
using MontyHallProblemSimulation.Domain.DomainService.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.Service.Imps
{
    public class MontyHallProblemSimulationService : IMontyHallProblemSimulationService
    {
        private readonly IRandomNumberGeneratorService randomNumberGenerator;

        public MontyHallProblemSimulationService(IRandomNumberGeneratorService randomNumberGenerator)
        {
            this.randomNumberGenerator = randomNumberGenerator;
        }

        private SimulationResponse CalculateSimulationResponse(List<Container> containers, int numberOfSimulations, bool changeDoor)
        {
            SimulationResponse response = new SimulationResponse()
            {
                NumberOfSimulations = numberOfSimulations,
                FailCount = 0,
                SuccessCount = 0,
            };

            foreach (var container in containers)
            {
                ContainerState containerState = null;
                if (changeDoor)
                {
                    containerState = container.ContainerStates.First(x => x.StateOfDoor == DoorState.NONE);
                }
                else
                {
                    containerState = container.ContainerStates.First(x => x.StateOfDoor == DoorState.PICKED);
                }

                if (containerState.StateOfPayload == PayloadState.CAR)
                {
                    response.SuccessCount++;
                }
                else
                {
                    response.FailCount++;
                }
            }

            return response;
        }

        private List<Container> ChangeContainerStates(int numberOfSimulations, List<int[]> randomNumbers)
        {
            List<Container> containers = new List<Container>();

            for (int i = 0; i < numberOfSimulations; i++)
            {
                var container = new Container()
                {
                    ContainerStates = new List<ContainerState>()
                };

                for (int ii = 0; ii < 3; ii++)
                {
                    container.ContainerStates.Add(new ContainerState()
                    {
                        StateOfDoor = DoorState.NONE,
                        StateOfPayload = PayloadState.GOAT
                    });
                }

                container.ContainerStates[randomNumbers[0][i]].StateOfDoor = DoorState.PICKED;
                container.ContainerStates[randomNumbers[1][i]].StateOfPayload = PayloadState.CAR;

                if (randomNumbers[0][i] == randomNumbers[1][i])
                {
                    if (randomNumbers[2][i] == 0)
                    {
                        container.ContainerStates.Where(x => x.StateOfPayload == PayloadState.GOAT).First().StateOfDoor = DoorState.OPENED;
                    }
                    else
                    {
                        container.ContainerStates.Where(x => x.StateOfPayload == PayloadState.GOAT).Last().StateOfDoor = DoorState.OPENED;
                    }
                }
                else
                {
                    container.ContainerStates.First(x => x.StateOfPayload == PayloadState.GOAT && x.StateOfDoor == DoorState.NONE).StateOfDoor = DoorState.OPENED;
                }

                containers.Add(container);
            }

            return containers;
        }

        public SimulationResponse SimulateMontyHallProblem(int numberOfSimulations, bool changeDoor)
        {
            List<RandomNumberGeneratorPayload> randomNumberPayloads = new List<RandomNumberGeneratorPayload>()
            {
                //StateOfDoor: PICKED
                new RandomNumberGeneratorPayload()
                {
                    MinValue = 0,
                    MaxValue = 3,
                    Total = numberOfSimulations,
                },
                //StateOfPayload: CAR
                new RandomNumberGeneratorPayload()
                {
                    MinValue = 0,
                    MaxValue = 3,
                    Total = numberOfSimulations,
                },
                new RandomNumberGeneratorPayload()
                {
                    MinValue = 0,
                    MaxValue = 2,
                    Total = numberOfSimulations,
                }
            };

            var randomNumbers = this.randomNumberGenerator.GenerateRandomNumbers(randomNumberPayloads);

            List<Container> containers = this.ChangeContainerStates(numberOfSimulations, randomNumbers);

            SimulationResponse response = this.CalculateSimulationResponse(containers, numberOfSimulations, changeDoor);

            return response;
        }
    }
}
