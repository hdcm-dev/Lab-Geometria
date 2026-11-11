using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeometriaFactory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConceptTrace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HuellaDeConsulta",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Anillo = table.Column<int>(type: "INTEGER", nullable: false),
                    Intencion = table.Column<string>(type: "TEXT", maxLength: 48, nullable: false),
                    Forma = table.Column<int>(type: "INTEGER", nullable: false),
                    Resultado = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    VersionDelCorpus = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                    Dia = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuellaDeConsulta", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HuellaDeConsulta_Dia",
                table: "HuellaDeConsulta",
                column: "Dia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuellaDeConsulta");
        }
    }
}
