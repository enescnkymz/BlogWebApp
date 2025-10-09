using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class editing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_newsLetters",
                table: "newsLetters");

            migrationBuilder.RenameTable(
                name: "newsLetters",
                newName: "NewsLetters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsLetters",
                table: "NewsLetters",
                column: "MailID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsLetters",
                table: "NewsLetters");

            migrationBuilder.RenameTable(
                name: "NewsLetters",
                newName: "newsLetters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_newsLetters",
                table: "newsLetters",
                column: "MailID");
        }
    }
}
