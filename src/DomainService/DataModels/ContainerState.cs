using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.DataModels
{
    public class ContainerState
    {
        public DoorState StateOfDoor { get; set; }

        public PayloadState StateOfPayload { get; set; }
    }
}
