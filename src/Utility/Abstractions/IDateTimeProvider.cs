using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Shared.Utility.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime GetUtcDateTime();
    }
}
