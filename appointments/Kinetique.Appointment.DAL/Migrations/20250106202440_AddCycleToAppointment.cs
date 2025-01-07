using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Appointment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCycleToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentCycles_AppointmentCycleId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentCycleId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentCycleId",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.AddColumn<long>(
                name: "CycleId",
                table: "Appointment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CycleId",
                table: "Appointment",
                column: "CycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AppointmentCycles_CycleId",
                table: "Appointment",
                column: "CycleId",
                principalTable: "AppointmentCycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AppointmentCycles_CycleId",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_CycleId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "CycleId",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.AddColumn<long>(
                name: "AppointmentCycleId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

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
    }
}
