using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServicioApiPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class NuevosUsuarios3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 4, 3, 8, 256, DateTimeKind.Local).AddTicks(3239));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 4, 3, 8, 256, DateTimeKind.Local).AddTicks(3253));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 4, 3, 8, 256, DateTimeKind.Local).AddTicks(3255));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 4, 3, 8, 256, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 4, 3, 8, 256, DateTimeKind.Local).AddTicks(1559));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "PasswordHash", "Rol", "Usuario" },
                values: new object[,]
                {
                    { 5, "$2a$11$10nuLwUvSwh79v05XB28s.JYmGyYGizJNHyprjHZPK4pnbLTcPq/K", "admin", "admin" },
                    { 6, "$2a$11$VJKfcXk6/hjkH9zpI/GMY.CjY5ymEHsfAKy/XqZjEVIQZixTmkfc2", "user", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 50, 6, 361, DateTimeKind.Local).AddTicks(7146));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 50, 6, 361, DateTimeKind.Local).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 50, 6, 361, DateTimeKind.Local).AddTicks(7156));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 50, 6, 361, DateTimeKind.Local).AddTicks(5535));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 50, 6, 361, DateTimeKind.Local).AddTicks(5552));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "PasswordHash", "Rol", "Usuario" },
                values: new object[,]
                {
                    { 3, "$2a$11$zpQ1W5ke1nEaJ1b4pw.xzezZgpbK04go8tTCjunhDJ3vCQ8xQLexK", "user", "user2" },
                    { 4, "$2a$11$zzQoLrFGe/aSRftmTi4AfuKFzYk7hXi91ULCHbq9PtN1crBag9pS2", "user", "user3" }
                });
        }
    }
}
