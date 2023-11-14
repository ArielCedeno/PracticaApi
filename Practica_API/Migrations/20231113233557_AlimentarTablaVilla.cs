using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practica_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa...", new DateTime(2023, 11, 13, 18, 35, 56, 885, DateTimeKind.Local).AddTicks(8416), new DateTime(2023, 11, 13, 18, 35, 56, 885, DateTimeKind.Local).AddTicks(8406), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "", "Detalle de la villa...", new DateTime(2023, 11, 13, 18, 35, 56, 885, DateTimeKind.Local).AddTicks(8419), new DateTime(2023, 11, 13, 18, 35, 56, 885, DateTimeKind.Local).AddTicks(8418), "", 35, "Vista a la playa", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
