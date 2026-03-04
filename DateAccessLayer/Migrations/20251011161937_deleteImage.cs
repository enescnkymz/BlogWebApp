using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class deleteImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionThumbnailImage",
                table: "Descriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionThumbnailImage",
                table: "Descriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
