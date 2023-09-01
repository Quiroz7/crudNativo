using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallerNativo.Migrations
{
    /// <inheritdoc />
    public partial class mitienda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdClientes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__5EB79C21B9C521B5", x => x.IdClientes);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProductos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__718C7D0727652181", x => x.IdProductos);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    IdVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IdClientes = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ventas__BC1240BD7BFA1BAF", x => x.IdVenta);
                    table.ForeignKey(
                        name: "FK__Ventas__ClienteI__5070F446",
                        column: x => x.IdClientes,
                        principalTable: "Clientes",
                        principalColumn: "IdClientes");
                });

            migrationBuilder.CreateTable(
                name: "DetallesVentas",
                columns: table => new
                {
                    DetalleVentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalles__340EEDA4E98A6609", x => x.DetalleVentaId);
                    table.ForeignKey(
                        name: "FK__DetallesV__Produ__5441852A",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "IdProductos");
                    table.ForeignKey(
                        name: "FK__DetallesV__Venta__534D60F1",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "IdVenta");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Clientes__B4ADFE3853ED35A9",
                table: "Clientes",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_ProductoId",
                table: "DetallesVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_VentaId",
                table: "DetallesVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "UQ__Producto__06370DACB42AAE4F",
                table: "Productos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdClientes",
                table: "Ventas",
                column: "IdClientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesVentas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
