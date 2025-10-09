using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionID",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_DescriptionID",
                table: "Questions",
                column: "DescriptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Descriptions_DescriptionID",
                table: "Questions",
                column: "DescriptionID",
                principalTable: "Descriptions",
                principalColumn: "DescriptionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Descriptions_DescriptionID",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_DescriptionID",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "DescriptionID",
                table: "Questions");
        }
    }
}
