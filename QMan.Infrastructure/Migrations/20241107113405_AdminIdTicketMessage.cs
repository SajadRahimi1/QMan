using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminIdTicketMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TicketMessages",
                newName: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "TicketMessages",
                newName: "UserId");
        }
    }
}
