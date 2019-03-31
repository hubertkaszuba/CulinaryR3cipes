using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryR3cipes.Migrations
{
    public partial class ReportCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isSubmitted",
                table: "Recipes",
                newName: "IsSubmitted");

            migrationBuilder.AlterColumn<int>(
                name: "Time",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Img",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportsCounter",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportsCounter",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportsCounter",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ReportsCounter",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "IsSubmitted",
                table: "Recipes",
                newName: "isSubmitted");

            migrationBuilder.AlterColumn<int>(
                name: "Time",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Img",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
