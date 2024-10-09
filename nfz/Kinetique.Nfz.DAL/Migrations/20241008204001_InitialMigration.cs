using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kinetique.Nfz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProcedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Treatment = table.Column<string>(type: "text", nullable: false),
                    PatientProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticProcedures_PatientProcedures_PatientProcedureId",
                        column: x => x.PatientProcedureId,
                        principalTable: "PatientProcedures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SettlementProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<decimal>(type: "numeric", nullable: false),
                    StatisticProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementProcedures_StatisticProcedures_StatisticProcedure~",
                        column: x => x.StatisticProcedureId,
                        principalTable: "StatisticProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettlementProcedures_StatisticProcedureId",
                table: "SettlementProcedures",
                column: "StatisticProcedureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatisticProcedures_PatientProcedureId",
                table: "StatisticProcedures",
                column: "PatientProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticProcedures_Treatment",
                table: "StatisticProcedures",
                column: "Treatment",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettlementProcedures");

            migrationBuilder.DropTable(
                name: "StatisticProcedures");

            migrationBuilder.DropTable(
                name: "PatientProcedures");
        }
    }
}
