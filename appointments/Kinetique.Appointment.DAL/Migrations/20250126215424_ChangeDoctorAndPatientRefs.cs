using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Appointment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDoctorAndPatientRefs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AppointmentCycles");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "AppointmentCycles");

            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "AppointmentCycles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientPesel",
                table: "AppointmentCycles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "AppointmentCycles");

            migrationBuilder.DropColumn(
                name: "PatientPesel",
                table: "AppointmentCycles");

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
                nullable: false,
                defaultValue: 0L);
        }
    }
}
