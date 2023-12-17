using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _RoBotland.Migrations
{
    /// <inheritdoc />
    public partial class userFixed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDetails_UserDetailsId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cbe456d5-9962-44fb-a6cf-0ddb0dc56146"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("675fe797-57c5-4f2e-ba33-ad9dfd32ae80"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserDetails_UserDetailsId",
                table: "Orders",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDetails_UserDetailsId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("675fe797-57c5-4f2e-ba33-ad9dfd32ae80"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("cbe456d5-9962-44fb-a6cf-0ddb0dc56146"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserDetails_UserDetailsId",
                table: "Orders",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
