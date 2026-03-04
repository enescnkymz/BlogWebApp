using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class writercomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Writers_CommentSenderID",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Writers_CommentSenderID",
                table: "Comments",
                column: "CommentSenderID",
                principalTable: "Writers",
                principalColumn: "WriterID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Writers_CommentSenderID",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Writers_CommentSenderID",
                table: "Comments",
                column: "CommentSenderID",
                principalTable: "Writers",
                principalColumn: "WriterID");
        }
    }
}
