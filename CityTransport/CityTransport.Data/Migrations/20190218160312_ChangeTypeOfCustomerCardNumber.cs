using Microsoft.EntityFrameworkCore.Migrations;

namespace CityTransport.Data.Migrations
{
    public partial class ChangeTypeOfCustomerCardNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerCardNumber",
                table: "CustomerCards",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CustomerCardNumber",
                table: "CustomerCards",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
