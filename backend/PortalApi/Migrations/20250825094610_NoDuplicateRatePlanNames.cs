using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanterburyUnderwater.PortalApi.Migrations
{
    /// <inheritdoc />
    public partial class NoDuplicateRatePlanNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookingRatePlans_Name",
                table: "BookingRatePlans",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookingRatePlans_Name",
                table: "BookingRatePlans");
        }
    }
}
