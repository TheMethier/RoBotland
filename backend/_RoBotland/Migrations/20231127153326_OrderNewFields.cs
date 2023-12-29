using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _RoBotland.Migrations
{
    /// <inheritdoc />
    public partial class OrderNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9cbe7e88-6475-4011-8e7e-b7e6eba8660e"));

            migrationBuilder.RenameColumn(
                name: "HomeAdress",
                table: "UserDetails",
                newName: "HomeAddress");

            migrationBuilder.AddColumn<Guid>(
                name: "UserDetailsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("d6546f2b-56d2-456e-95fb-49c47045d1aa"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d6546f2b-56d2-456e-95fb-49c47045d1aa"));

            migrationBuilder.DropColumn(
                name: "UserDetailsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "HomeAddress",
                table: "UserDetails",
                newName: "HomeAdress");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("9cbe7e88-6475-4011-8e7e-b7e6eba8660e"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, "ADMIN" });
        }
    }
}
