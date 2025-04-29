using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookOrGetBooked.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TryingToMakeServiceSeedWorkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_User_ProviderId",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "Services",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Services",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_ProviderId",
                table: "Services",
                column: "ProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_User_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_ProviderId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_User_UserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_UserId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "ProviderId",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_User_ProviderId",
                table: "Services",
                column: "ProviderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
