using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDescriptionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments",
                column: "DescriptionID",
                principalTable: "Descriptions",
                principalColumn: "DescriptionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments",
                column: "DescriptionID",
                principalTable: "Descriptions",
                principalColumn: "DescriptionID");
        }
    }
}
