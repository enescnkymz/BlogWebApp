using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class editNot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Writers_WriterID",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_WriterID",
                table: "Notifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notifications_WriterID",
                table: "Notifications",
                column: "WriterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Writers_WriterID",
                table: "Notifications",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "WriterID");
        }
    }
}
