using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddShareToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d8598afe-27b3-4467-831e-33841a8c406c");

            migrationBuilder.AddColumn<string>(
                name: "ShareToken",
                table: "PropertyListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpirationDate",
                table: "PropertyListings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7ccbcb73-1a84-40bc-8361-0ec4dacc26ad", 0, "3bd4cfb5-28af-4c62-95df-18341014441a", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEPsUvfl9YJ8IYNH7cvKO437gtZhipOxiHhvmMnyWSuldked8mbdD+sVvaHj9Lc4uEg==", "5537531375", false, "8da61664-d01e-420f-b884-b2fbb4ffdb4d", "[]", false, "adminBerke" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ccbcb73-1a84-40bc-8361-0ec4dacc26ad");

            migrationBuilder.DropColumn(
                name: "ShareToken",
                table: "PropertyListings");

            migrationBuilder.DropColumn(
                name: "TokenExpirationDate",
                table: "PropertyListings");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d8598afe-27b3-4467-831e-33841a8c406c", 0, "9a5bb5ea-2779-41db-a654-c30dc511fd15", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEOkFpBZDBC5u+OEEpHWXW3rdCuIAlu8Yz55caythtuW8VJI7AG1Yxg9wbAun8bXkKw==", "5537531375", false, "9c196266-01c6-457c-9956-c2d005cf9a17", "[]", false, "adminBerke" });
        }
    }
}
