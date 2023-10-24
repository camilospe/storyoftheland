using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesOfTheLand.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specimen",
                columns: table => new
                {
                    SpecimenID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CulturalSignificance = table.Column<string>(type: "TEXT", maxLength: 3500, nullable: false),
                    LatinName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specimen", x => x.SpecimenID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specimen");
        }
    }
}
