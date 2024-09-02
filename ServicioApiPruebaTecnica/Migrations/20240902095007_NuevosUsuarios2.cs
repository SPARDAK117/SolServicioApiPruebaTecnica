using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioApiPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class NuevosUsuarios2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$zpQ1W5ke1nEaJ1b4pw.xzezZgpbK04go8tTCjunhDJ3vCQ8xQLexK");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$zzQoLrFGe/aSRftmTi4AfuKFzYk7hXi91ULCHbq9PtN1crBag9pS2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$0VAktWpFAaKN2yBTMBe3Zuw9lgXzp4YByEjr9Kh2Zn6weix4J1ade");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$CEC3Wm5ZdKQJapV3KFNFoO71hOW6KqT1yKl8amtM7ycP.66W12rYe");
        }
    }
}
