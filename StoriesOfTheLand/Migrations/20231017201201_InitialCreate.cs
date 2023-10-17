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
                    SpecimenName = table.Column<string>(type: "TEXT", nullable: false),
                    SpecimenDescription = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false)
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
