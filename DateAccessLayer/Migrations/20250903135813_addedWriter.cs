using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addedWriter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.CreateTable(
                name: "Writers",
                columns: table => new
                {
                    WriterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WriterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterAbout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writers", x => x.WriterID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Writers");

            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    DealerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerAbout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.DealerID);
                });
        }
    }
}
