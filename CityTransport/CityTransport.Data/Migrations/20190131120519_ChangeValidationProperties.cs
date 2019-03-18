using Microsoft.EntityFrameworkCore.Migrations;

namespace CityTransport.Data.Migrations
{
    public partial class ChangeValidationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LineNumber",
                table: "Lines",
                newName: "LineName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LineName",
                table: "Lines",
                newName: "LineNumber");
        }
    }
}
