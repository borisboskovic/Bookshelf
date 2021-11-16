using Microsoft.EntityFrameworkCore.Migrations;

namespace BookshelfAPI.Data.Migrations
{
    public partial class BookIssueTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BookIssue",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "BookIssue");
        }
    }
}
