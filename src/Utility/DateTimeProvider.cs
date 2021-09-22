using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;

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
