﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServicioApiPruebaTecnica.Data;

#nullable disable

namespace ServicioApiPruebaTecnica.Migrations
{
    [DbContext(typeof(PruebaTecnicaOMCContextDB))]
    partial class PruebaTecnicaOMCContextDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("LogEntries", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Action = "Created initial log entry",
                            Timestamp = new DateTime(2024, 9, 2, 19, 43, 32, 120, DateTimeKind.Utc).AddTicks(6378),
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("ServicioApiPruebaTecnica.Data.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ProductoName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Productos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProductoName = "Producto A",
                            SKU = "A123",
                            created_at = new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(8605),
                            deleted_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            updated_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            ProductoName = "Producto B",
                            SKU = "B456",
                            created_at = new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(8618),
                            deleted_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            updated_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            ProductoName = "Producto C",
                            SKU = "C789",
                            created_at = new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(8620),
                            deleted_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            updated_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ServicioApiPruebaTecnica.Data.Sucursal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SucursalName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sucursales", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Direccion = "Calle Xola 23#",
                            SucursalName = "Xola",
                            Telefono = "5546354636",
                            created_at = new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(6732),
                            updated_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Direccion = "Calle Chilpanginco 23#",
                            SucursalName = "Chilpancigo",
                            Telefono = "5532165487",
                            created_at = new DateTime(2024, 9, 2, 13, 43, 31, 795, DateTimeKind.Local).AddTicks(6756),
                            updated_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ServicioApiPruebaTecnica.Data.SucursalProducto", b =>
                {
                    b.Property<int>("SucursalId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("SucursalId", "ProductoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("SucursalProductos", (string)null);
                });

            modelBuilder.Entity("ServicioApiPruebaTecnica.Data.Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("user")
                        .HasColumnName("Rol");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Usuario");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Usuarios", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "$2a$11$jCgbyEnQDkn3sF66Z5J.Jebl6HTPsyt9wtapkBKsIRA7KN/NmSCAG",
                            Role = "admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = "$2a$11$ShWff.3sILzuPcboUP1GK.HpQFw.MKIThsir3Uk9aafOX6VAtwmKu",
                            Role = "user",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("ServicioApiPruebaTecnica.Data.SucursalProducto", b =>
                {
                    b.HasOne("ServicioApiPruebaTecnica.Data.Producto", "Producto")
                        .WithMany("SucursalProductos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicioApiPruebaTecnica.Data.Sucursal", "Sucursal")
                        .WithMany("SucursalProductos")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("ServicioApiPruebaTecnica.Data.Producto", b =>
                {
                    b.Navigation("SucursalProductos");
                });

            modelBuilder.Entity("ServicioApiPruebaTecnica.Data.Sucursal", b =>
                {
                    b.Navigation("SucursalProductos");
                });
#pragma warning restore 612, 618
        }
    }
}
