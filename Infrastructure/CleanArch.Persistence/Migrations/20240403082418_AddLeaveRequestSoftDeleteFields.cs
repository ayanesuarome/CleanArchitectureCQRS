using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveRequestSoftDeleteFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "LeaveRequests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeaveRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 4, 3, 11, 24, 17, 361, DateTimeKind.Unspecified).AddTicks(1557), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 3, 11, 24, 17, 361, DateTimeKind.Unspecified).AddTicks(1749), new TimeSpan(0, 3, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_IsDeleted",
                table: "LeaveRequests",
                column: "IsDeleted",
                filter: "IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_IsDeleted",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeaveRequests");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 2, 5, 15, 20, 13, 164, DateTimeKind.Unspecified).AddTicks(2000), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 5, 15, 20, 13, 164, DateTimeKind.Unspecified).AddTicks(2075), new TimeSpan(0, 2, 0, 0, 0)) });
        }
    }
}
