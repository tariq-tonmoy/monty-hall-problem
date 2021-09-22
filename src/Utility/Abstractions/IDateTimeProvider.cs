using System;

namespace MontyHallProblemSimulation.Shared.Utility.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime GetUtcDateTime();
    }
}
