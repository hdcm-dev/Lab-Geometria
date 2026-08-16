using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeometriaFactory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Interpretation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Observacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Kind = table.Column<string>(type: "TEXT", nullable: false),
                    PiecePosition = table.Column<int>(type: "INTEGER", nullable: true),
                    Field = table.Column<string>(type: "TEXT", nullable: false),
                    DeclaredValue = table.Column<double>(type: "REAL", nullable: true),
                    DerivedValue = table.Column<double>(type: "REAL", nullable: true),
                    WorkId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observacion_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pieza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    DeclaredArea = table.Column<double>(type: "REAL", nullable: true),
                    DerivedArea = table.Column<double>(type: "REAL", nullable: true),
                    DeclaredVolume = table.Column<double>(type: "REAL", nullable: true),
                    DerivedVolume = table.Column<double>(type: "REAL", nullable: true),
                    WorkId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pieza_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Componente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    DeclaredLength = table.Column<double>(type: "REAL", nullable: true),
                    DeclaredWidth = table.Column<double>(type: "REAL", nullable: true),
                    DeclaredRadius = table.Column<double>(type: "REAL", nullable: true),
                    DeclaredArea = table.Column<double>(type: "REAL", nullable: true),
                    PieceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componente_Pieza_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Componente_PieceId",
                table: "Componente",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_Observacion_WorkId",
                table: "Observacion",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Pieza_WorkId_Position",
                table: "Pieza",
                columns: new[] { "WorkId", "Position" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Componente");

            migrationBuilder.DropTable(
                name: "Observacion");

            migrationBuilder.DropTable(
                name: "Pieza");
        }
    }
}
