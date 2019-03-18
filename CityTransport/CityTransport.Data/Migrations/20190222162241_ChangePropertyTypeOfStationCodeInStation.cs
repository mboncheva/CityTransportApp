using Microsoft.EntityFrameworkCore.Migrations;

namespace CityTransport.Data.Migrations
{
    public partial class ChangePropertyTypeOfStationCodeInStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StationCode",
                table: "Stations",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StationCode",
                table: "Stations",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
