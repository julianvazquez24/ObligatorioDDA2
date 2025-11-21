using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;



#nullable disable

namespace ObligatorioDDA2.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class cambiosModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "respuesta",
                table: "Preguntas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "respuestaMatematica",
                table: "Preguntas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "respuesta",
                table: "Preguntas");

            migrationBuilder.DropColumn(
                name: "respuestaMatematica",
                table: "Preguntas");
        }
    }
}
