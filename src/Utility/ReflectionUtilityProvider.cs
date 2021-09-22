using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;

namespace MontyHallProblemSimulation.Shared.Utility
{
    internal class ReflectionUtilityProvider : IReflectionUtilityProvider
    {
        public string GetFullyQualifiedAssemblyName(Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}
