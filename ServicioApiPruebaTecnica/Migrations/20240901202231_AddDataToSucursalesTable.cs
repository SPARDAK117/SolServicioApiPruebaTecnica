using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServicioApiPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToSucursalesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sucursales",
                columns: new[] { "Id", "Direccion", "SucursalName", "Telefono", "created_at", "updated_at" },
                values: new object[,]
                {
                    { 1, "Calle Xola 23#", "Xola", "5546354636", new DateTime(2024, 9, 1, 14, 22, 31, 147, DateTimeKind.Local).AddTicks(1407), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Calle Chilpanginco 23#", "Chilpancigo", "5532165487", new DateTime(2024, 9, 1, 14, 22, 31, 147, DateTimeKind.Local).AddTicks(1423), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
