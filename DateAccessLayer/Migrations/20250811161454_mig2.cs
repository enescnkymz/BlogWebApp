using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Descriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_CategoryID",
                table: "Descriptions",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptions_Categories_CategoryID",
                table: "Descriptions",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Descriptions_Categories_CategoryID",
                table: "Descriptions");

            migrationBuilder.DropIndex(
                name: "IX_Descriptions_CategoryID",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Descriptions");
        }
    }
}
