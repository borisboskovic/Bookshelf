using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookshelfAPI.Data.Migrations
{
    public partial class OriginalReleaseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Book",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYearOnly",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ReleaseYearOnly",
                table: "Book");
        }
    }
}
