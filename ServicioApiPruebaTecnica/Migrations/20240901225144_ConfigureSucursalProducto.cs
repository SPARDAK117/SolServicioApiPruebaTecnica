using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioApiPruebaTecnica.Migrations
{
    public partial class ConfigureSucursalProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SucursalProductos_Sucursales_SucursalId1",
                table: "SucursalProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SucursalProductos",
                table: "SucursalProductos");

            migrationBuilder.DropIndex(
                name: "IX_SucursalProductos_SucursalId1",
                table: "SucursalProductos");

            migrationBuilder.DropColumn(
                name: "SucursalId1",
                table: "SucursalProductos");

            // Eliminar el intento de modificar la columna SucursalId
            // migrationBuilder.AlterColumn<int>(
            //    name: "SucursalId",
            //    table: "SucursalProductos",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SucursalProductos",
                table: "SucursalProductos",
                columns: new[] { "SucursalId", "ProductoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SucursalProductos_Sucursales_SucursalId",
                table: "SucursalProductos",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SucursalProductos_Sucursales_SucursalId",
                table: "SucursalProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SucursalProductos",
                table: "SucursalProductos");

            migrationBuilder.AlterColumn<int>(
                name: "SucursalId",
                table: "SucursalProductos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SucursalId1",
                table: "SucursalProductos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SucursalProductos",
                table: "SucursalProductos",
                column: "SucursalId");

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "ProductoName", "SKU", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { 1, "Producto A", "A123", new DateTime(2024, 9, 1, 16, 48, 48, 166, DateTimeKind.Local).AddTicks(2233), DateTime.MinValue, DateTime.MinValue },
                    { 2, "Producto B", "B456", new DateTime(2024, 9, 1, 16, 48, 48, 166, DateTimeKind.Local).AddTicks(2248), DateTime.MinValue, DateTime.MinValue },
                    { 3, "Producto C", "C789", new DateTime(2024, 9, 1, 16, 48, 48, 166, DateTimeKind.Local).AddTicks(2250), DateTime.MinValue, DateTime.MinValue }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SucursalProductos_SucursalId1",
                table: "SucursalProductos",
                column: "SucursalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SucursalProductos_Sucursales_SucursalId1",
                table: "SucursalProductos",
                column: "SucursalId1",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

