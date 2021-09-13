using Microsoft.EntityFrameworkCore.Migrations;

namespace VacationManagerApi.Migrations
{
    public partial class RemoveYearfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Vacations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
