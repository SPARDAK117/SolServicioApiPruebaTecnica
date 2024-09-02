using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServicioApiPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuariosTable : Migration
    {
        /// <inheritdoc />
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "user",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "LogEntries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "LogEntries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "LogEntries",
                columns: new[] { "Id", "Action", "Timestamp", "Username" },
                values: new object[] { 1, "Created initial log entry", new DateTime(2024, 9, 2, 19, 43, 32, 120, DateTimeKind.Utc).AddTicks(6378), "admin" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(8605));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(8618));

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(8620));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(6732));

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(6756));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "PasswordHash", "Rol", "Usuario" },
                values: new object[,]
                {
                    { 1, "$2a$11$jCgbyEnQDkn3sF66Z5J.Jebl6HTPsyt9wtapkBKsIRA7KN/NmSCAG", "admin", "admin" },
                    { 2, "$2a$11$ShWff.3sILzuPcboUP1GK.HpQFw.MKIThsir3Uk9aafOX6VAtwmKu", "user", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Usuario",
                table: "Usuarios",
                column: "Usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Usuario",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "user");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "LogEntries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "LogEntries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

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
    }
}
