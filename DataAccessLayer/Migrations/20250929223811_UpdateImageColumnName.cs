using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "114f5b27-bb41-4f69-a0ef-58a949b036db");

            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "PropertyListings",
                newName: "Images");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d8598afe-27b3-4467-831e-33841a8c406c", 0, "9a5bb5ea-2779-41db-a654-c30dc511fd15", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEOkFpBZDBC5u+OEEpHWXW3rdCuIAlu8Yz55caythtuW8VJI7AG1Yxg9wbAun8bXkKw==", "5537531375", false, "9c196266-01c6-457c-9956-c2d005cf9a17", "[]", false, "adminBerke" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d8598afe-27b3-4467-831e-33841a8c406c");

            migrationBuilder.RenameColumn(
                name: "Images",
                table: "PropertyListings",
                newName: "ImageBase64");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IncomeExpenses", "IsOnline", "LastActiveDate", "LockoutEnabled", "LockoutEnd", "NameSurname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tasks", "TwoFactorEnabled", "UserName" },
                values: new object[] { "114f5b27-bb41-4f69-a0ef-58a949b036db", 0, "591c16f7-bde7-4c73-9a29-cb384cb89646", "berke.yildirimm44@gmail.com", false, "[]", false, null, false, null, "Berke Yıldırım", null, "ADMIN", "AQAAAAIAAYagAAAAEPHfwI9gMMv4ztJPZyLbqZpzZQb88ybszn0uDESFQjFRyztC2KP71AHHvuCuDIYWlA==", "5537531375", false, "e20c28e6-b765-40d4-8216-d602df20ec8c", "[]", false, "adminBerke" });
        }
    }
}
