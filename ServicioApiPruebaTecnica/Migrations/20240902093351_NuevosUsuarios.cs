using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServicioApiPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class NuevosUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 33, 49, 884, DateTimeKind.Local).AddTicks(9292));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 33, 49, 884, DateTimeKind.Local).AddTicks(9301));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 33, 49, 884, DateTimeKind.Local).AddTicks(9302));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 33, 49, 884, DateTimeKind.Local).AddTicks(7501));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 3, 33, 49, 884, DateTimeKind.Local).AddTicks(7521));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "PasswordHash", "Rol", "Usuario" },
                values: new object[,]
                {
                    { 3, "$2a$11$0VAktWpFAaKN2yBTMBe3Zuw9lgXzp4YByEjr9Kh2Zn6weix4J1ade", "user", "user2" },
                    { 4, "$2a$11$CEC3Wm5ZdKQJapV3KFNFoO71hOW6KqT1yKl8amtM7ycP.66W12rYe", "user", "user3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2024, 9, 1, 18, 56, 10, 999, DateTimeKind.Local).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 18, 56, 10, 999, DateTimeKind.Local).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 18, 56, 10, 999, DateTimeKind.Local).AddTicks(6378));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 18, 56, 10, 999, DateTimeKind.Local).AddTicks(4338));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 18, 56, 10, 999, DateTimeKind.Local).AddTicks(4353));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "PasswordHash", "Rol", "Usuario" },
                values: new object[] { 1, "$2a$11$T5XrZAr6Tt3i4x0a/Bh98.kyqWaiQPNhuaBIsk98bHTkynvgNSxRG", "admin", "admin" });
        }
    }
}
