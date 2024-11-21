using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmaciaVerifarmaChallenge.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FarmaciaMigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farmacias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(8,5)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(9,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmacias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Farmacias");
        }
    }
}
