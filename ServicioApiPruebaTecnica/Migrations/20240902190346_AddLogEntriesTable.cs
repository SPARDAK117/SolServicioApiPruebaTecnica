using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioApiPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class AddLogEntriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 17, 1, 53, 247, DateTimeKind.Local).AddTicks(7835));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 17, 1, 53, 247, DateTimeKind.Local).AddTicks(7839));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 17, 1, 53, 247, DateTimeKind.Local).AddTicks(7841));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 17, 1, 53, 247, DateTimeKind.Local).AddTicks(6601));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 17, 1, 53, 247, DateTimeKind.Local).AddTicks(6618));
        }
    }
}
