using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeometriaFactory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CredentialChangedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CredentialChangedAt",
                table: "Account",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CredentialChangedAt",
                table: "Account");
        }
    }
}
