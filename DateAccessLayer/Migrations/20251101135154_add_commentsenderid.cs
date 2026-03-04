using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_commentsenderid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentSenderID",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentSenderID",
                table: "Comments",
                column: "CommentSenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Writers_CommentSenderID",
                table: "Comments",
                column: "CommentSenderID",
                principalTable: "Writers",
                principalColumn: "WriterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Writers_CommentSenderID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentSenderID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentSenderID",
                table: "Comments");
        }
    }
}
