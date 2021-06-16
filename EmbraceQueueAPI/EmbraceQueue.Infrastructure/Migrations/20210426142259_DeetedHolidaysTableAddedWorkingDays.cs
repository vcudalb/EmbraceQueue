using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmbraceQueue.Infrastructure.Migrations
{
    public partial class DeetedHolidaysTableAddedWorkingDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "494ba0e8-d1bc-4fd9-a0a3-441ea76d7626");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6ef1f52f-febd-45b8-8cc5-71b54884d32c");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "818a1cd7-0337-4b3c-9bcb-0ed2840d133d");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "fbeb44bb-43b5-498e-9741-ed30036355d8");

            migrationBuilder.CreateTable(
                name: "WorkingDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BreakEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakStartTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingDays_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dc58d3f3-68ad-4b3f-a663-162b1bd63a8d", "d047d6a6-d6fe-4967-8588-8bfd8ff19b5c", "enduser", "ENDUSER" },
                    { "7a9c54b5-3b49-4240-a0a2-e0b252f96141", "d39d29a2-788c-4316-aea4-54dcb4cb6541", "helpdeskemployee", "HELPDESKEMPLOYEE" },
                    { "2b8b14c0-26b5-4a70-b060-ac9fd4c4cb52", "2b849e49-2b67-411f-b184-b594f6e03a0f", "branchmanager", "BRANCHMANAGER" },
                    { "8f0ad5a7-8e2d-4084-81da-cbd96b6e5094", "5f1155fc-43d9-46b0-97da-0d44a639ec6c", "superadmin", "SUPERADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_BranchId",
                table: "WorkingDays",
                column: "BranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingDays");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2b8b14c0-26b5-4a70-b060-ac9fd4c4cb52");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7a9c54b5-3b49-4240-a0a2-e0b252f96141");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8f0ad5a7-8e2d-4084-81da-cbd96b6e5094");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dc58d3f3-68ad-4b3f-a663-162b1bd63a8d");

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "494ba0e8-d1bc-4fd9-a0a3-441ea76d7626", "2d14e171-f06a-4fb1-93a9-78beeb5604dc", "enduser", "ENDUSER" },
                    { "fbeb44bb-43b5-498e-9741-ed30036355d8", "14cafbba-28b1-4820-88fb-c3d2994f4dac", "helpdeskemployee", "HELPDESKEMPLOYEE" },
                    { "6ef1f52f-febd-45b8-8cc5-71b54884d32c", "c0b60502-5026-45ce-b934-78eee60564ba", "branchmanager", "BRANCHMANAGER" },
                    { "818a1cd7-0337-4b3c-9bcb-0ed2840d133d", "d314885e-c5db-4fac-a622-b1496c84bcf9", "superadmin", "SUPERADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_BranchId",
                table: "Holidays",
                column: "BranchId");
        }
    }
}
