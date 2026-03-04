using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class editrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Descriptions_Writers_WriterID",
                table: "Descriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments",
                column: "DescriptionID",
                principalTable: "Descriptions",
                principalColumn: "DescriptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptions_Writers_WriterID",
                table: "Descriptions",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "WriterID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Descriptions_Writers_WriterID",
                table: "Descriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Descriptions_DescriptionID",
                table: "Comments",
                column: "DescriptionID",
                principalTable: "Descriptions",
                principalColumn: "DescriptionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptions_Writers_WriterID",
                table: "Descriptions",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "WriterID");
        }
    }
}
