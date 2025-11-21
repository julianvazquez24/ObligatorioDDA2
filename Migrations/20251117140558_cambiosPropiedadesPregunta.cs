using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;



#nullable disable

namespace ObligatorioDDA2.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class cambiosPropiedadesPregunta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo_proposicion",
                table: "Preguntas");

            migrationBuilder.DropColumn(
                name: "proposicion",
                table: "Preguntas");

            migrationBuilder.DropColumn(
                name: "respuestaMatematica",
                table: "Preguntas");

            migrationBuilder.DropColumn(
                name: "secuencia",
                table: "Preguntas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigo_proposicion",
                table: "Preguntas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "proposicion",
                table: "Preguntas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "respuestaMatematica",
                table: "Preguntas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "secuencia",
                table: "Preguntas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
