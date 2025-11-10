using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioDDA2.Migrations
{
    /// <inheritdoc />
    public partial class migracionUno : Migration
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
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numeros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    secuencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pregunta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codigo_pregunta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proposicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codigo_proposicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
