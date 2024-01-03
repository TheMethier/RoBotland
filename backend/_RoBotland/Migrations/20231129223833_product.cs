using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _RoBotland.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Product_ProductsId",
                table: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d6546f2b-56d2-456e-95fb-49c47045d1aa"));

            migrationBuilder.AddColumn<string>(
                name: "IsAvailable",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("d24e721a-f062-4c2b-b9ef-81885f4164e2"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Products_ProductsId",
                table: "ProductCategory",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Products_ProductsId",
                table: "ProductCategory");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d24e721a-f062-4c2b-b9ef-81885f4164e2"));

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("d6546f2b-56d2-456e-95fb-49c47045d1aa"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Product_ProductsId",
                table: "ProductCategory",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
