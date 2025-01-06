using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kinetique.Appointment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddReferalToCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReferralId",
                table: "AppointmentCycles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Referral",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uid = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Pesel = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referral", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentCycles_Referral_ReferralId",
                table: "AppointmentCycles");

            migrationBuilder.DropTable(
                name: "Referral");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentCycles_ReferralId",
                table: "AppointmentCycles");

            migrationBuilder.DropColumn(
                name: "ReferralId",
                table: "AppointmentCycles");
        }
    }
}
