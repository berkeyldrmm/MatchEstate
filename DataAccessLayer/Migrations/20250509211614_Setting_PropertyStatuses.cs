using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Setting_PropertyStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyListings_PropertyStatus_PropertyStatusId",
                table: "PropertyListings");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequests_PropertyStatus_PropertyStatusId",
                table: "PropertyRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyStatus",
                table: "PropertyStatus");

            migrationBuilder.RenameTable(
                name: "PropertyStatus",
                newName: "PropertyStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyStatuses",
                table: "PropertyStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyListings_PropertyStatuses_PropertyStatusId",
                table: "PropertyListings",
                column: "PropertyStatusId",
                principalTable: "PropertyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequests_PropertyStatuses_PropertyStatusId",
                table: "PropertyRequests",
                column: "PropertyStatusId",
                principalTable: "PropertyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyListings_PropertyStatuses_PropertyStatusId",
                table: "PropertyListings");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequests_PropertyStatuses_PropertyStatusId",
                table: "PropertyRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyStatuses",
                table: "PropertyStatuses");

            migrationBuilder.RenameTable(
                name: "PropertyStatuses",
                newName: "PropertyStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyStatus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyStatus",
                table: "PropertyStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyListings_PropertyStatus_PropertyStatusId",
                table: "PropertyListings",
                column: "PropertyStatusId",
                principalTable: "PropertyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequests_PropertyStatus_PropertyStatusId",
                table: "PropertyRequests",
                column: "PropertyStatusId",
                principalTable: "PropertyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
