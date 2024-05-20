//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace CleanArch.Persistence.Migrations
//{
//    /// <inheritdoc />
//    public partial class Aux : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "LeaveTypes",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
//                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
//                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    DefaultDays = table.Column<int>(type: "int", maxLength: 100, nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "LeaveAllocations",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
//                    Period = table.Column<int>(type: "int", nullable: false),
//                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
//                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
//                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_LeaveAllocations", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
//                        column: x => x.LeaveTypeId,
//                        principalTable: "LeaveTypes",
//                        principalColumn: "Id");
//                });

//            migrationBuilder.CreateTable(
//                name: "LeaveRequests",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Comments = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
//                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
//                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
//                    RequestingEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
//                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
//                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
//                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
//                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    LeaveTypeName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
//                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
//                    StartDate = table.Column<DateOnly>(type: "date", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
//                        column: x => x.LeaveTypeId,
//                        principalTable: "LeaveTypes",
//                        principalColumn: "Id");
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_LeaveAllocations_LeaveTypeId",
//                table: "LeaveAllocations",
//                column: "LeaveTypeId");

//            migrationBuilder.CreateIndex(
//                name: "IX_LeaveRequests_IsDeleted",
//                table: "LeaveRequests",
//                column: "IsDeleted",
//                filter: "IsDeleted = 0");

//            migrationBuilder.CreateIndex(
//                name: "IX_LeaveRequests_LeaveTypeId",
//                table: "LeaveRequests",
//                column: "LeaveTypeId");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "LeaveAllocations");

//            migrationBuilder.DropTable(
//                name: "LeaveRequests");

//            migrationBuilder.DropTable(
//                name: "LeaveTypes");
//        }
//    }
//}
