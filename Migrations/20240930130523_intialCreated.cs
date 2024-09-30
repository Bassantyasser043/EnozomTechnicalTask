using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoctorAvailabiltity.Migrations
{
    /// <inheritdoc />
    public partial class intialCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DayName = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DoctorName = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TimeRanges",
                columns: table => new
                {
                    TimeRangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    From = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    To = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRanges", x => x.TimeRangeId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DoctorAvailabilities",
                columns: table => new
                {
                    DoctorAvailabilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    TimeRangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorAvailabilities", x => x.DoctorAvailabilityId);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_TimeRanges_TimeRangeId",
                        column: x => x.TimeRangeId,
                        principalTable: "TimeRanges",
                        principalColumn: "TimeRangeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "DayName" },
                values: new object[,]
                {
                    { 1, "Sunday" },
                    { 2, "Munday" },
                    { 3, "TuesDay" },
                    { 4, "Wednesday" },
                    { 5, "Thursday" },
                    { 6, "Friday" },
                    { 7, "Saturday" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "DoctorName" },
                values: new object[,]
                {
                    { 1, "Mohamed" },
                    { 2, "Ahmed" }
                });

            migrationBuilder.InsertData(
                table: "TimeRanges",
                columns: new[] { "TimeRangeId", "From", "To" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0) },
                    { 2, new TimeSpan(0, 14, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0) },
                    { 3, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 21, 0, 0, 0) },
                    { 4, new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "DoctorAvailabilities",
                columns: new[] { "DoctorAvailabilityId", "DayId", "DoctorId", "TimeRangeId" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 3, 1, 1 },
                    { 4, 3, 1, 2 },
                    { 5, 3, 1, 3 },
                    { 6, 4, 1, 1 },
                    { 7, 4, 1, 2 },
                    { 8, 5, 1, 2 },
                    { 9, 6, 1, 1 },
                    { 10, 2, 2, 4 },
                    { 11, 3, 2, 1 },
                    { 12, 3, 2, 3 },
                    { 13, 5, 2, 2 },
                    { 14, 6, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_DayId",
                table: "DoctorAvailabilities",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_DoctorId",
                table: "DoctorAvailabilities",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_TimeRangeId",
                table: "DoctorAvailabilities",
                column: "TimeRangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorAvailabilities");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "TimeRanges");
        }
    }
}
