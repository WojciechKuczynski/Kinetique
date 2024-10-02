using System;
using System.Collections.Generic;
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
                name: "SettlemenetProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<decimal>(type: "numeric", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlemenetProcedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticProcedureGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codes = table.Column<List<string>>(type: "text[]", nullable: false),
                    SettlemenetProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    PatientProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticProcedureGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticProcedureGroups_PatientProcedures_PatientProcedure~",
                        column: x => x.PatientProcedureId,
                        principalTable: "PatientProcedures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StatisticProcedureGroups_SettlemenetProcedures_SettlemenetP~",
                        column: x => x.SettlemenetProcedureId,
                        principalTable: "SettlemenetProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticProcedureGroups_PatientProcedureId",
                table: "StatisticProcedureGroups",
                column: "PatientProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticProcedureGroups_SettlemenetProcedureId",
                table: "StatisticProcedureGroups",
                column: "SettlemenetProcedureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticProcedureGroups");

            migrationBuilder.DropTable(
                name: "PatientProcedures");

            migrationBuilder.DropTable(
                name: "SettlemenetProcedures");
        }
    }
}
