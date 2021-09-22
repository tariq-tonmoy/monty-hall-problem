using System;

namespace MontyHallProblemSimulation.Infrastructure.Core
{
    public interface IEntityBase
    {
        Guid Id { get; }

        int Version { get; }

        Guid CreatedBy { get; }

        Guid LastUpdatedBy { get; }

        DateTime CreatedDate { get; }

        DateTime LastUpdatedDate { get; }

        bool IsMarkedToDelete { get; }
    }
}
