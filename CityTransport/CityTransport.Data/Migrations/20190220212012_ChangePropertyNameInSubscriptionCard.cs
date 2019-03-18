using Microsoft.EntityFrameworkCore.Migrations;

namespace CityTransport.Data.Migrations
{
    public partial class ChangePropertyNameInSubscriptionCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidateCard",
                table: "SubscriptionCards");

            migrationBuilder.AddColumn<int>(
                name: "ValidityCard",
                table: "SubscriptionCards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidityCard",
                table: "SubscriptionCards");

            migrationBuilder.AddColumn<int>(
                name: "ValidateCard",
                table: "SubscriptionCards",
                nullable: false,
                defaultValue: 0);
        }
    }
}
