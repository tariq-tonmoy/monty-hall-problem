using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.DataModels
{
    public class RandomNumberGeneratorPayload
    {
        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public int Total { get; set; }
    }
}
