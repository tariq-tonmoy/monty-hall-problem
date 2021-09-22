using System;

namespace MontyHallProblemSimulation.Shared.Utility.Abstractions
{
    public interface IReflectionUtilityProvider
    {
        string GetFullyQualifiedAssemblyName(Type type);
    }
}
