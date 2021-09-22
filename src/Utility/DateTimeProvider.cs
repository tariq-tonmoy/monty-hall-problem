using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Shared.Utility
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetUtcDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
