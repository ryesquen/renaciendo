using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RenaciendoWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class datos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Renaceres",
                columns: new[] { "RenacerId", "Amenidad", "Detalle", "Dimension", "FechaActualizacion", "FechaCreacion", "ImageURL", "Nombre" },
                values: new object[,]
                {
                    { 1, "Chiste de 🐈‍", "Siempre", 123, new DateTime(2023, 4, 13, 20, 20, 33, 101, DateTimeKind.Local).AddTicks(1787), new DateTime(2023, 4, 13, 20, 20, 33, 101, DateTimeKind.Local).AddTicks(1776), null, "Manzanita" },
                    { 2, "Chiste de 🍏", "Constante", 321, new DateTime(2023, 4, 13, 20, 20, 33, 101, DateTimeKind.Local).AddTicks(1790), new DateTime(2023, 4, 13, 20, 20, 33, 101, DateTimeKind.Local).AddTicks(1789), null, "Mau" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Renaceres",
                keyColumn: "RenacerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Renaceres",
                keyColumn: "RenacerId",
                keyValue: 2);
        }
    }
}
