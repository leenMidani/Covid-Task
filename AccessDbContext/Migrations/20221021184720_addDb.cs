using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COVID.Migrations
{
    public partial class addDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmiratesID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Vacciene",
                columns: table => new
                {
                    VaccieneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccieneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveMonths = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacciene", x => x.VaccieneId);
                });

            migrationBuilder.CreateTable(
                name: "PatientHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccineId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId1 = table.Column<int>(type: "int", nullable: false),
                    VaccieneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientHistories_Patients_PatientId1",
                        column: x => x.PatientId1,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientHistories_Vacciene_VaccieneId",
                        column: x => x.VaccieneId,
                        principalTable: "Vacciene",
                        principalColumn: "VaccieneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientHistories_PatientId1",
                table: "PatientHistories",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_PatientHistories_VaccieneId",
                table: "PatientHistories",
                column: "VaccieneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientHistories");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Vacciene");
        }
    }
}
