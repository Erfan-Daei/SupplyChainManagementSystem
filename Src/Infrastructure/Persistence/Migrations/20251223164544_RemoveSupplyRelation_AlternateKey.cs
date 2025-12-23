using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSupplyRelation_AlternateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SupplyRelations_ServiceId_ConsumerCompanyId",
                table: "SupplyRelations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 23, 16, 45, 43, 722, DateTimeKind.Utc).AddTicks(4137),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 21, 10, 54, 57, 30, DateTimeKind.Utc).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 45, 43, 722, DateTimeKind.Utc).AddTicks(7483));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 45, 43, 722, DateTimeKind.Utc).AddTicks(8900));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 45, 43, 722, DateTimeKind.Utc).AddTicks(8902));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 45, 43, 722, DateTimeKind.Utc).AddTicks(8903));

            migrationBuilder.CreateIndex(
                name: "IX_SupplyRelations_ServiceId",
                table: "SupplyRelations",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplyRelations_ServiceId",
                table: "SupplyRelations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 21, 10, 54, 57, 30, DateTimeKind.Utc).AddTicks(7923),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 23, 16, 45, 43, 722, DateTimeKind.Utc).AddTicks(4137));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SupplyRelations_ServiceId_ConsumerCompanyId",
                table: "SupplyRelations",
                columns: new[] { "ServiceId", "ConsumerCompanyId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 10, 54, 57, 31, DateTimeKind.Utc).AddTicks(909));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 10, 54, 57, 31, DateTimeKind.Utc).AddTicks(2324));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 10, 54, 57, 31, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 21, 10, 54, 57, 31, DateTimeKind.Utc).AddTicks(2327));
        }
    }
}
