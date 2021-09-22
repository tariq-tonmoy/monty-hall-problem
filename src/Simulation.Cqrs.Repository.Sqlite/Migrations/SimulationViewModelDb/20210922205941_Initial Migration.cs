using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MontyHallProblemSimulation.Infrastructure.Simulation.Cqrs.Repository.Sqlite.Migrations.SimulationViewModelDb
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulationViewModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DoorChanged = table.Column<bool>(type: "INTEGER", nullable: false),
                    SuccessCount = table.Column<long>(type: "INTEGER", nullable: false),
                    FailCount = table.Column<long>(type: "INTEGER", nullable: false),
                    NumberOfSimulations = table.Column<long>(type: "INTEGER", nullable: false),
                    SuccessRatio = table.Column<double>(type: "REAL", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsMarkedToDelete = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationViewModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationViewModels");
        }
    }
}
