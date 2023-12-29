using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _RoBotland.Migrations
{
    /// <inheritdoc />
    public partial class ADMIN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("9cbe7e88-6475-4011-8e7e-b7e6eba8660e"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9cbe7e88-6475-4011-8e7e-b7e6eba8660e"));
        }
    }
}
