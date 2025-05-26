using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixingRequestIdSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyListings_PropertyRequestId",
                table: "PropertyListings");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyListings_PropertyRequestId",
                table: "PropertyListings",
                column: "PropertyRequestId",
                unique: true,
                filter: "[PropertyRequestId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyListings_PropertyRequestId",
                table: "PropertyListings");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyListings_PropertyRequestId",
                table: "PropertyListings",
                column: "PropertyRequestId");
        }
    }
}
