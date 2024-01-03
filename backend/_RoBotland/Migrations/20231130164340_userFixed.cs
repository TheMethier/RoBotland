using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _RoBotland.Migrations
{
    /// <inheritdoc />
    public partial class userFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d24e721a-f062-4c2b-b9ef-81885f4164e2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("cbe456d5-9962-44fb-a6cf-0ddb0dc56146"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cbe456d5-9962-44fb-a6cf-0ddb0dc56146"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("d24e721a-f062-4c2b-b9ef-81885f4164e2"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });
        }
    }
}
