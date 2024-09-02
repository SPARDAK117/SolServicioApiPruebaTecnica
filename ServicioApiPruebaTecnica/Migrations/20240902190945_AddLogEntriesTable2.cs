using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioApiPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class AddLogEntriesTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 9, 44, 235, DateTimeKind.Local).AddTicks(5651));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 9, 44, 235, DateTimeKind.Local).AddTicks(5656));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 9, 44, 235, DateTimeKind.Local).AddTicks(5658));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 9, 44, 235, DateTimeKind.Local).AddTicks(4495));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 9, 44, 235, DateTimeKind.Local).AddTicks(4509));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 3, 45, 455, DateTimeKind.Local).AddTicks(5853));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 3, 45, 455, DateTimeKind.Local).AddTicks(5860));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 3, 45, 455, DateTimeKind.Local).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 3, 45, 455, DateTimeKind.Local).AddTicks(4796));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 3, 45, 455, DateTimeKind.Local).AddTicks(4813));
        }
    }
}
