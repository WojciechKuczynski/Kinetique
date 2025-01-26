using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Schedule.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDoctorIdToCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "DoctorSchedules");

            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "DoctorSchedules",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "DoctorSchedules");

            migrationBuilder.AddColumn<long>(
                name: "DoctorId",
                table: "DoctorSchedules",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
