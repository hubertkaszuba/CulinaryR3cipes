using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryR3cipes.Migrations
{
    public partial class UserBan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "AspNetUsers");
        }
    }
}
