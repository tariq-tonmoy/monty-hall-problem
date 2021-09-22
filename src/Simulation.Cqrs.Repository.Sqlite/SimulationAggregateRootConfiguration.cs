using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontyHallProblemSimulation.Domain.SimulationAggregateRoot;
using System;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite
{
    public class SimulationAggregateRootConfiguration : IEntityTypeConfiguration<SimulationAggregateRoot>
    {
        public void Configure(EntityTypeBuilder<SimulationAggregateRoot> builder)
        {
            builder.ToTable($"{typeof(SimulationAggregateRoot).Name}s");
            builder.HasKey(aggregateRoot => aggregateRoot.Id);
            builder.Ignore(b => b.DomainEvents);

            builder
            .Property<Guid>(nameof(SimulationAggregateRoot.Id))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.Id))
            .IsRequired(true);

            builder
            .Property<int>(nameof(SimulationAggregateRoot.Version))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.Version))
            .IsRequired(true);

            builder
            .Property<Guid>(nameof(SimulationAggregateRoot.CreatedBy))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.CreatedBy))
            .IsRequired(true);

            builder
            .Property<DateTime>(nameof(SimulationAggregateRoot.CreatedDate))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.CreatedDate))
            .IsRequired(true);

            builder
            .Property<Guid>(nameof(SimulationAggregateRoot.LastUpdatedBy))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.LastUpdatedBy))
            .IsRequired(true);

            builder
            .Property<DateTime>(nameof(SimulationAggregateRoot.LastUpdatedDate))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.LastUpdatedDate))
            .IsRequired(true);

            builder
            .Property<bool>(nameof(SimulationAggregateRoot.IsMarkedToDelete))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.IsMarkedToDelete))
            .IsRequired(true);

            builder
            .Property<long>(nameof(SimulationAggregateRoot.SuccessCount))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.SuccessCount))
            .IsRequired(true);

            builder
            .Property<long>(nameof(SimulationAggregateRoot.FailCount))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.FailCount))
            .IsRequired(true);

            builder
            .Property<long>(nameof(SimulationAggregateRoot.NumberOfSimulations))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.NumberOfSimulations))
            .IsRequired(true);

            builder
            .Property<double>(nameof(SimulationAggregateRoot.SuccessRatio))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.SuccessRatio))
            .IsRequired(true);

            builder
            .Property<bool>(nameof(SimulationAggregateRoot.DoorChanged))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationAggregateRoot.DoorChanged))
            .IsRequired(true);
        }
    }
}
