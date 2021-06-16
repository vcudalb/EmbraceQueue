using Microsoft.EntityFrameworkCore.Migrations;

namespace EmbraceQueue.Infrastructure.Migrations
{
    public partial class FixedBranchManagerRoleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "139a2880-6218-4ce6-98c1-addef2ec2287");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "74492d6f-0f7a-4c70-a0f6-9339c3cf43c9");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "88a864e7-92e7-4432-843d-81cbe1c2dc24");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8df3a6df-5335-4fe0-998e-94f02519d032");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8df3a6df-5335-4fe0-998e-94f02519d032", "d852c5e8-8828-413d-a4ae-611d8835002c", "enduser", "ENDUSER" },
                    { "88a864e7-92e7-4432-843d-81cbe1c2dc24", "f8e3e5a2-1f84-4d9e-bc24-a049a13ead36", "helpdeskemployee", "HELPDESKEMPLOYEE" },
                    { "139a2880-6218-4ce6-98c1-addef2ec2287", "d46f6658-6b87-44de-b29c-6068810b7274", "branchmanager ", "BRANCHMANAGER" },
                    { "74492d6f-0f7a-4c70-a0f6-9339c3cf43c9", "207988af-ae2c-4e03-8426-98b7b330da55", "superadmin", "SUPERADMIN" }
                });
        }
    }
}
