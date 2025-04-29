using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookOrGetBooked.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceCoverage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceCoverage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxDrivingDistanceKm = table.Column<double>(type: "REAL", nullable: false),
                    MaxDrivingTimeMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    ProviderLatitude = table.Column<double>(type: "REAL", nullable: false),
                    ProviderLongitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCoverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCoverage_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCoverage_ServiceId",
                table: "ServiceCoverage",
                column: "ServiceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCoverage");
        }
    }
}
