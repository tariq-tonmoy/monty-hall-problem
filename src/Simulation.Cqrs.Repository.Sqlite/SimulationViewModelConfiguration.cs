using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontyHallProblemSimulation.ReadSide.ViewModel;
using System;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite
{
    public class SimulationViewModelConfiguration : IEntityTypeConfiguration<SimulationViewModel>
    {
        public void Configure(EntityTypeBuilder<SimulationViewModel> builder)
        {
            builder.ToTable($"{typeof(SimulationViewModel).Name}s");
            builder.HasKey(ViewModel => ViewModel.Id);

            builder
            .Property<Guid>(nameof(SimulationViewModel.Id))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.Id))
            .IsRequired(true);

            builder
            .Property<int>(nameof(SimulationViewModel.Version))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.Version))
            .IsRequired(true);

            builder
            .Property<Guid>(nameof(SimulationViewModel.CreatedBy))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.CreatedBy))
            .IsRequired(true);

            builder
            .Property<DateTime>(nameof(SimulationViewModel.CreatedDate))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.CreatedDate))
            .IsRequired(true);

            builder
            .Property<Guid>(nameof(SimulationViewModel.LastUpdatedBy))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.LastUpdatedBy))
            .IsRequired(true);

            builder
            .Property<DateTime>(nameof(SimulationViewModel.LastUpdatedDate))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.LastUpdatedDate))
            .IsRequired(true);

            builder
            .Property<bool>(nameof(SimulationViewModel.IsMarkedToDelete))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.IsMarkedToDelete))
            .IsRequired(true);

            builder
            .Property<long>(nameof(SimulationViewModel.SuccessCount))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.SuccessCount))
            .IsRequired(true);

            builder
            .Property<long>(nameof(SimulationViewModel.FailCount))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.FailCount))
            .IsRequired(true);

            builder
            .Property<long>(nameof(SimulationViewModel.NumberOfSimulations))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.NumberOfSimulations))
            .IsRequired(true);

            builder
            .Property<double>(nameof(SimulationViewModel.SuccessRatio))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.SuccessRatio))
            .IsRequired(true);

            builder
            .Property<bool>(nameof(SimulationViewModel.DoorChanged))
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName(nameof(SimulationViewModel.DoorChanged))
            .IsRequired(true);
        }
    }
}
