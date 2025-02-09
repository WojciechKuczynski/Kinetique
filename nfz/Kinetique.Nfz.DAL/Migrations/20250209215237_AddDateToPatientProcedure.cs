using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Nfz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToPatientProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "PatientProcedures",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "PatientProcedures",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "PatientProcedures");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PatientProcedures");
        }
    }
}
