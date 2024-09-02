using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveRequestConcurrencyTokenColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "LeaveRequests",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "LeaveRequests");
        }
    }
}
