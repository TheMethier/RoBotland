using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _RoBotland.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9cbe7e88-6475-4011-8e7e-b7e6eba8660e"));

            migrationBuilder.AddColumn<int>(
                name: "IsAvailable",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductDtoId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("155834a3-3cf6-45a5-aed0-cbbed18f42f0"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductDtoId",
                table: "Categories",
                column: "ProductDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductDtoId",
                table: "Categories",
                column: "ProductDtoId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductDtoId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductDtoId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("155834a3-3cf6-45a5-aed0-cbbed18f42f0"));

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDtoId",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("9cbe7e88-6475-4011-8e7e-b7e6eba8660e"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, "ADMIN" });
        }
    }
}
