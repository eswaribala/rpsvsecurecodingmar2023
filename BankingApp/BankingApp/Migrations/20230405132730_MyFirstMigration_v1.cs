using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Migrations
{
    public partial class MyFirstMigration_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanType",
                table: "Corporate",
                newName: "CompanyType");

            migrationBuilder.AddColumn<string>(
                name: "LandMark",
                table: "Address",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandMark",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "CompanyType",
                table: "Corporate",
                newName: "CompanType");
        }
    }
}
