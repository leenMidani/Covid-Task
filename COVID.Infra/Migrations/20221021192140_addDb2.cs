using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COVID.Migrations
{
    public partial class addDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientHistories_Patients_PatientId1",
                table: "PatientHistories");

            migrationBuilder.DropIndex(
                name: "IX_PatientHistories_PatientId1",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "PatientHistories");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "HistoryId",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PatientHistories_PatientId",
                table: "PatientHistories",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientHistories_Patients_PatientId",
                table: "PatientHistories",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientHistories_Patients_PatientId",
                table: "PatientHistories");

            migrationBuilder.DropIndex(
                name: "IX_PatientHistories_PatientId",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "PatientHistories");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PatientId1",
                table: "PatientHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientHistories_PatientId1",
                table: "PatientHistories",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientHistories_Patients_PatientId1",
                table: "PatientHistories",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
