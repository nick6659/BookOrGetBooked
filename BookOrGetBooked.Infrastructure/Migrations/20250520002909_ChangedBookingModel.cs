using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookOrGetBooked.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceAddress",
                table: "Bookings",
                newName: "StreetAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Bookings",
                newName: "ServiceAddress");
        }
    }
}
