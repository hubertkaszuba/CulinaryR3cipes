using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryR3cipes.Migrations
{
    public partial class Fridge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSubmitted",
                table: "Recipes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Fridges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSubmitted",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Fridges");
        }
    }
}
