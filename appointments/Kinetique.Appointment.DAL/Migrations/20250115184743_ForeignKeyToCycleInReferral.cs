using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Appointment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyToCycleInReferral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentCycles_Referral_ReferralId",
                table: "AppointmentCycles");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentCycles_ReferralId",
                table: "AppointmentCycles");

            migrationBuilder.DropColumn(
                name: "ReferralId",
                table: "AppointmentCycles");

            migrationBuilder.AddColumn<long>(
                name: "AppointmentCycleId",
                table: "Referral",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Referral_AppointmentCycleId",
                table: "Referral",
                column: "AppointmentCycleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Referral_AppointmentCycles_AppointmentCycleId",
                table: "Referral",
                column: "AppointmentCycleId",
                principalTable: "AppointmentCycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referral_AppointmentCycles_AppointmentCycleId",
                table: "Referral");

            migrationBuilder.DropIndex(
                name: "IX_Referral_AppointmentCycleId",
                table: "Referral");

            migrationBuilder.DropColumn(
                name: "AppointmentCycleId",
                table: "Referral");

            migrationBuilder.AddColumn<long>(
                name: "ReferralId",
                table: "AppointmentCycles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentCycles_ReferralId",
                table: "AppointmentCycles",
                column: "ReferralId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentCycles_Referral_ReferralId",
                table: "AppointmentCycles",
                column: "ReferralId",
                principalTable: "Referral",
                principalColumn: "Id");
        }
    }
}
