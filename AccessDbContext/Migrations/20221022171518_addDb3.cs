using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COVID.Migrations
{
    public partial class addDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "PatientHistories");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "PatientHistories",
                newName: "EntryCreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntryCreatedDate",
                table: "PatientHistories",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "HistoryId",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VaccineId",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
