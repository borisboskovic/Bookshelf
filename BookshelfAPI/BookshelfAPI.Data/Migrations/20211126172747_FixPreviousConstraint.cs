using Microsoft.EntityFrameworkCore.Migrations;

namespace BookshelfAPI.Data.Migrations
{
    public partial class FixPreviousConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COMMENTR_REACTIONO_REVIEWCO",
                table: "CommentReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_REVIEWCOMMENT",
                table: "ReviewComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COMMENTREACTION",
                table: "CommentReaction");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "CommentReaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_REVIEWCOMMENT",
                table: "ReviewComment",
                columns: new[] { "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_COMMENTREACTION",
                table: "CommentReaction",
                columns: new[] { "User_Id", "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id", "ReviewId" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReaction_Book_Id_BookIssue_Id_Review_User_Id_CommentAuthor_Id_ReviewId",
                table: "CommentReaction",
                columns: new[] { "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id", "ReviewId" });

            migrationBuilder.AddForeignKey(
                name: "FK_COMMENTR_REACTIONO_REVIEWCO",
                table: "CommentReaction",
                columns: new[] { "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id", "ReviewId" },
                principalTable: "ReviewComment",
                principalColumns: new[] { "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id", "Id" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COMMENTR_REACTIONO_REVIEWCO",
                table: "CommentReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_REVIEWCOMMENT",
                table: "ReviewComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COMMENTREACTION",
                table: "CommentReaction");

            migrationBuilder.DropIndex(
                name: "IX_CommentReaction_Book_Id_BookIssue_Id_Review_User_Id_CommentAuthor_Id_ReviewId",
                table: "CommentReaction");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "CommentReaction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_REVIEWCOMMENT",
                table: "ReviewComment",
                columns: new[] { "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_COMMENTREACTION",
                table: "CommentReaction",
                columns: new[] { "User_Id", "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_COMMENTR_REACTIONO_REVIEWCO",
                table: "CommentReaction",
                columns: new[] { "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id" },
                principalTable: "ReviewComment",
                principalColumns: new[] { "Book_Id", "BookIssue_Id", "Review_User_Id", "CommentAuthor_Id" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
