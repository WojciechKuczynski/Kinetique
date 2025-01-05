using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kinetique.Appointment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddReferalToPatientProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AppointmentCycleId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppointmentCycles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Limit = table.Column<byte>(type: "smallint", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentCycles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentCycleId",
                table: "Appointments",
                column: "AppointmentCycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentCycles_AppointmentCycleId",
                table: "Appointments",
                column: "AppointmentCycleId",
                principalTable: "AppointmentCycles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentCycles_AppointmentCycleId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "AppointmentCycles");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentCycleId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentCycleId",
                table: "Appointments");
        }
    }
}
