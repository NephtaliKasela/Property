using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelBetwReservationAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_applicationUserId",
                table: "Reservations",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_applicationUserId",
                table: "Reservations",
                column: "applicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_applicationUserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_applicationUserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "Reservations");
        }
    }
}
