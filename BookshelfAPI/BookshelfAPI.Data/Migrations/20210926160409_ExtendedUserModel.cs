using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookshelfAPI.Data.Migrations
{
    public partial class ExtendedUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CredentialsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CredentialsHistory_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LanguageId",
                table: "AspNetUsers",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialsHistory_UserId1",
                table: "CredentialsHistory",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Language_LanguageId",
                table: "AspNetUsers",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Language_LanguageId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CredentialsHistory");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LanguageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "AspNetUsers");
        }
    }
}
