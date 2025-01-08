using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Appointment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MovePatientAndDoctorToCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointment");

            migrationBuilder.AddColumn<bool>(
                name: "CycleFull",
                table: "AppointmentCycles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DoctorId",
                table: "AppointmentCycles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PatientId",
                table: "AppointmentCycles",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CycleFull",
                table: "AppointmentCycles");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AppointmentCycles");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "AppointmentCycles");

            migrationBuilder.AddColumn<long>(
                name: "DoctorId",
                table: "Appointment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PatientId",
                table: "Appointment",
                type: "bigint",
                nullable: true);
        }
    }
}
