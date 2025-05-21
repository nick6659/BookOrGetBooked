using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookOrGetBooked.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPreferredCurrencyToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ServiceTypes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefined",
                table: "ServiceTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PreferredCurrencyId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_CreatedByUserId",
                table: "ServiceTypes",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_AspNetUsers_CreatedByUserId",
                table: "ServiceTypes",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_AspNetUsers_CreatedByUserId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTypes_CreatedByUserId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "IsSystemDefined",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "PreferredCurrencyId",
                table: "AspNetUsers");
        }
    }
}
