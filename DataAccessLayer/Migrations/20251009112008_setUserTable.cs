using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class setUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ccbcb73-1a84-40bc-8361-0ec4dacc26ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e09e8e8b-1ed7-4a2f-b2fd-92e47228fa45", "AQAAAAIAAYagAAAAEPwZZFryFv96B2o0ftzaEM8gLstcedFD1pQZD5GUDi/LcS+v4Ejpk0In1C61ox+/Xg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ccbcb73-1a84-40bc-8361-0ec4dacc26ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3bd4cfb5-28af-4c62-95df-18341014441a", "AQAAAAIAAYagAAAAEPsUvfl9YJ8IYNH7cvKO437gtZhipOxiHhvmMnyWSuldked8mbdD+sVvaHj9Lc4uEg==" });
        }
    }
}
