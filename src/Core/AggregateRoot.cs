using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;
using System.Collections.Generic;

namespace MontyHallProblemSimulation.Infrastructure.Core
{
    public abstract class AggregateRoot : IEntityBase
    {
        private List<DomainEvent> domainEvents;

        protected void AddEvent(DomainEvent @event)
        {
            if (domainEvents == null)
            {
                domainEvents = new List<DomainEvent>();
            }

            domainEvents.Add(@event);
            this.Version++;
        }

        public Guid Id { get; protected set; }

        public int Version { get; private set; }

        public Guid CreatedBy { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public Guid LastUpdatedBy { get; protected set; }

        public DateTime LastUpdatedDate { get; protected set; }

        public bool IsMarkedToDelete { get; protected set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => domainEvents;

        protected void SetDefaultValues(Guid sessionId, IDateTimeProvider dateTimeProvider)
        {
            this.LastUpdatedBy = sessionId;
            this.LastUpdatedDate = dateTimeProvider.GetUtcDateTime();
            if (this.CreatedBy == Guid.Empty)
            {
                this.CreatedBy = sessionId;
                this.CreatedDate = dateTimeProvider.GetUtcDateTime();
                this.Version++;
            }
        }
    }
}
