using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanterburyUnderwater.PortalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirebaseUserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    HomePhone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    MobilePhone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    MembershipStatus = table.Column<int>(type: "integer", nullable: false),
                    MembershipStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MembershipEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSignedIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
