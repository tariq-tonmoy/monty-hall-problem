using System;

namespace MontyHallProblemSimulation.Infrastructure.Core
{
    public abstract class ViewModelBase : IEntityBase
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsMarkedToDelete { get; set; }
    }
}
