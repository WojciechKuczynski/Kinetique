using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetique.Schedule.Migrations
{
    /// <inheritdoc />
    public partial class AddBlocksToSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBlockers_DoctorSchedules_DoctorScheduleId",
                table: "ScheduleBlockers");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "ScheduleBlockers");

            migrationBuilder.AlterColumn<long>(
                name: "DoctorScheduleId",
                table: "ScheduleBlockers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBlockers_DoctorSchedules_DoctorScheduleId",
                table: "ScheduleBlockers",
                column: "DoctorScheduleId",
                principalTable: "DoctorSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBlockers_DoctorSchedules_DoctorScheduleId",
                table: "ScheduleBlockers");

            migrationBuilder.AlterColumn<long>(
                name: "DoctorScheduleId",
                table: "ScheduleBlockers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "DoctorId",
                table: "ScheduleBlockers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBlockers_DoctorSchedules_DoctorScheduleId",
                table: "ScheduleBlockers",
                column: "DoctorScheduleId",
                principalTable: "DoctorSchedules",
                principalColumn: "Id");
        }
    }
}
