using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioDDA2.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numeros = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secuencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codigo_pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    proposicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codigo_proposicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preguntas");
        }
    }
}
