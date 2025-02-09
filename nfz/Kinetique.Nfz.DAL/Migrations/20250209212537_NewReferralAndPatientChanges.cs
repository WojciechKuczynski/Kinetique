using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Nfz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewReferralAndPatientChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientProcedures_Referrals_ReferralId",
                table: "PatientProcedures");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "PatientProcedures");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "PatientProcedures",
                newName: "AppointmentExternalId");

            migrationBuilder.AlterColumn<long>(
                name: "ReferralId",
                table: "PatientProcedures",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "PatientPesel",
                table: "PatientProcedures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProcedures_Referrals_ReferralId",
                table: "PatientProcedures",
                column: "ReferralId",
                principalTable: "Referrals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientProcedures_Referrals_ReferralId",
                table: "PatientProcedures");

            migrationBuilder.DropColumn(
                name: "PatientPesel",
                table: "PatientProcedures");

            migrationBuilder.RenameColumn(
                name: "AppointmentExternalId",
                table: "PatientProcedures",
                newName: "PatientId");

            migrationBuilder.AlterColumn<long>(
                name: "ReferralId",
                table: "PatientProcedures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "PatientProcedures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProcedures_Referrals_ReferralId",
                table: "PatientProcedures",
                column: "ReferralId",
                principalTable: "Referrals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
