using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddingForignKeyRequestId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DealDate",
                table: "PropertyRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DealStatus",
                table: "PropertyRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PropertyRequestId",
                table: "PropertyListings",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "PropertyListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyListings_PropertyRequestId",
                table: "PropertyListings",
                column: "PropertyRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyListings_PropertyRequests_PropertyRequestId",
                table: "PropertyListings",
                column: "PropertyRequestId",
                principalTable: "PropertyRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyListings_PropertyRequests_PropertyRequestId",
                table: "PropertyListings");

            migrationBuilder.DropIndex(
                name: "IX_PropertyListings_PropertyRequestId",
                table: "PropertyListings");

            migrationBuilder.DropColumn(
                name: "DealDate",
                table: "PropertyRequests");

            migrationBuilder.DropColumn(
                name: "DealStatus",
                table: "PropertyRequests");

            migrationBuilder.DropColumn(
                name: "PropertyRequestId",
                table: "PropertyListings");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "PropertyListings");
        }
    }
}
