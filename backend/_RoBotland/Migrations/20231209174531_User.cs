using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _RoBotland.Migrations
{
    /// <inheritdoc />
    public partial class User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Users_Id",
                table: "UserDetails");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("675fe797-57c5-4f2e-ba33-ad9dfd32ae80"));

            migrationBuilder.DropColumn(
                name: "UserDetailsId",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("32887b79-031c-4534-9497-ab827a261491"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Users_Id",
                table: "UserDetails",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Users_Id",
                table: "UserDetails");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32887b79-031c-4534-9497-ab827a261491"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserDetailsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountBalance", "PasswordHash", "Role", "UserDetailsId", "Username" },
                values: new object[] { new Guid("675fe797-57c5-4f2e-ba33-ad9dfd32ae80"), 100000f, "$2a$11$sG0/Wsg4E9WWDC8NRJCGRu5Vgb78tf1UiLi1WTziC2xYNBukpqTOy", 1, new Guid("00000000-0000-0000-0000-000000000000"), "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Users_Id",
                table: "UserDetails",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
