﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TallerNativo.Models;

#nullable disable

namespace TallerNativo.Migrations
{
    [DbContext(typeof(TallerCrudContext))]
    [Migration("20230831131201_mitienda")]
    partial class mitienda
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TallerNativo.Models.Cliente", b =>
                {
                    b.Property<int>("IdClientes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClientes"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Cedula")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("IdClientes")
                        .HasName("PK__Clientes__5EB79C21B9C521B5");

                    b.HasIndex(new[] { "Cedula" }, "UQ__Clientes__B4ADFE3853ED35A9")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("TallerNativo.Models.DetallesVenta", b =>
                {
                    b.Property<int>("DetalleVentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetalleVentaId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("DetalleVentaId")
                        .HasName("PK__Detalles__340EEDA4E98A6609");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("DetallesVentas");
                });

            modelBuilder.Entity("TallerNativo.Models.Producto", b =>
                {
                    b.Property<int>("IdProductos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProductos"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.HasKey("IdProductos")
                        .HasName("PK__Producto__718C7D0727652181");

                    b.HasIndex(new[] { "Codigo" }, "UQ__Producto__06370DACB42AAE4F")
                        .IsUnique();

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("TallerNativo.Models.Venta", b =>
                {
                    b.Property<int>("IdVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVenta"));

                    b.Property<DateTime>("FechaVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdClientes")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("IdVenta")
                        .HasName("PK__Ventas__BC1240BD7BFA1BAF");

                    b.HasIndex("IdClientes");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("TallerNativo.Models.DetallesVenta", b =>
                {
                    b.HasOne("TallerNativo.Models.Producto", "Producto")
                        .WithMany("DetallesVenta")
                        .HasForeignKey("ProductoId")
                        .IsRequired()
                        .HasConstraintName("FK__DetallesV__Produ__5441852A");

                    b.HasOne("TallerNativo.Models.Venta", "Venta")
                        .WithMany("DetallesVenta")
                        .HasForeignKey("VentaId")
                        .IsRequired()
                        .HasConstraintName("FK__DetallesV__Venta__534D60F1");

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("TallerNativo.Models.Venta", b =>
                {
                    b.HasOne("TallerNativo.Models.Cliente", "Cliente")
                        .WithMany("Venta")
                        .HasForeignKey("IdClientes")
                        .IsRequired()
                        .HasConstraintName("FK__Ventas__ClienteI__5070F446");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("TallerNativo.Models.Cliente", b =>
                {
                    b.Navigation("Venta");
                });

            modelBuilder.Entity("TallerNativo.Models.Producto", b =>
                {
                    b.Navigation("DetallesVenta");
                });

            modelBuilder.Entity("TallerNativo.Models.Venta", b =>
                {
                    b.Navigation("DetallesVenta");
                });
#pragma warning restore 612, 618
        }
    }
}
