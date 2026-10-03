using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wellio.Migrations
{
    /// <inheritdoc />
    public partial class AddSleepIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SleepEntries_UserId",
                table: "SleepEntries");

            migrationBuilder.CreateIndex(
                name: "IX_SleepEntries_UserId_Date",
                table: "SleepEntries",
                columns: new[] { "UserId", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SleepEntries_UserId_Date",
                table: "SleepEntries");

            migrationBuilder.CreateIndex(
                name: "IX_SleepEntries_UserId",
                table: "SleepEntries",
                column: "UserId");
        }
    }
}
