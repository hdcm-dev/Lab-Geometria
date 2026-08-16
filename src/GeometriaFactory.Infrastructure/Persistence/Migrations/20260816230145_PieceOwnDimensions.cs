using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeometriaFactory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PieceOwnDimensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DeclaredLength",
                table: "Pieza",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DeclaredRadius",
                table: "Pieza",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DeclaredWidth",
                table: "Pieza",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeclaredLength",
                table: "Pieza");

            migrationBuilder.DropColumn(
                name: "DeclaredRadius",
                table: "Pieza");

            migrationBuilder.DropColumn(
                name: "DeclaredWidth",
                table: "Pieza");
        }
    }
}
