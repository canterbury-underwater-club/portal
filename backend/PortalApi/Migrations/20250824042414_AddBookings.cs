using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanterburyUnderwater.PortalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MembershipNumber",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingContractHolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingContractHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingRatePlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRatePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingFees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingRatePlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Basis = table.Column<int>(type: "integer", nullable: false),
                    UnitPriceCents = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingFees_BookingRatePlans_BookingRatePlanId",
                        column: x => x.BookingRatePlanId,
                        principalTable: "BookingRatePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingRatePlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    RateType = table.Column<int>(type: "integer", nullable: false),
                    ContractHolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    AttendeeType = table.Column<int>(type: "integer", nullable: true),
                    AgeBracket = table.Column<int>(type: "integer", nullable: true),
                    UnitPriceCents = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRates_BookingContractHolders_ContractHolderId",
                        column: x => x.ContractHolderId,
                        principalTable: "BookingContractHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingRates_BookingRatePlans_BookingRatePlanId",
                        column: x => x.BookingRatePlanId,
                        principalTable: "BookingRatePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckOutDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BookingStatus = table.Column<int>(type: "integer", nullable: false),
                    BondStatus = table.Column<int>(type: "integer", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Rooms = table.Column<HashSet<int>>(type: "integer[]", nullable: false),
                    PrimaryContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractHolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    BookingRatePlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_BookingContractHolders_ContractHolderId",
                        column: x => x.ContractHolderId,
                        principalTable: "BookingContractHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_BookingRatePlans_BookingRatePlanId",
                        column: x => x.BookingRatePlanId,
                        principalTable: "BookingRatePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_PrimaryContactId",
                        column: x => x.PrimaryContactId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingAttendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AttendeeType = table.Column<int>(type: "integer", nullable: true),
                    AgeBracket = table.Column<int>(type: "integer", nullable: true),
                    MembershipNumber = table.Column<int>(type: "integer", nullable: true),
                    LinkedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingAttendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingAttendees_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingAttendees_Users_LinkedUserId",
                        column: x => x.LinkedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingAttendees_BookingId",
                table: "BookingAttendees",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingAttendees_LinkedUserId",
                table: "BookingAttendees",
                column: "LinkedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContractHolders_Name",
                table: "BookingContractHolders",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingFees_BookingRatePlanId_Name",
                table: "BookingFees",
                columns: new[] { "BookingRatePlanId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRates_BookingRatePlanId_RateType_AttendeeType_AgeBra~",
                table: "BookingRates",
                columns: new[] { "BookingRatePlanId", "RateType", "AttendeeType", "AgeBracket" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRates_BookingRatePlanId_RateType_ContractHolderId",
                table: "BookingRates",
                columns: new[] { "BookingRatePlanId", "RateType", "ContractHolderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRates_ContractHolderId",
                table: "BookingRates",
                column: "ContractHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingRatePlanId",
                table: "Bookings",
                column: "BookingRatePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ContractHolderId",
                table: "Bookings",
                column: "ContractHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PrimaryContactId",
                table: "Bookings",
                column: "PrimaryContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingAttendees");

            migrationBuilder.DropTable(
                name: "BookingFees");

            migrationBuilder.DropTable(
                name: "BookingRates");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "BookingContractHolders");

            migrationBuilder.DropTable(
                name: "BookingRatePlans");

            migrationBuilder.DropColumn(
                name: "MembershipNumber",
                table: "Users");
        }
    }
}
