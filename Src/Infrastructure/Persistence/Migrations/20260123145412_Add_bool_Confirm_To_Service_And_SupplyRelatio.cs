using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_bool_Confirm_To_Service_And_SupplyRelatio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 23, 15, 4, 11, 196, DateTimeKind.Utc).AddTicks(9876),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 22, 9, 38, 51, 153, DateTimeKind.Utc).AddTicks(5592));

            migrationBuilder.AddColumn<bool>(
                name: "SupplyRelationIsConfirmed",
                table: "SupplyRelations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ServiceIsConfirmed",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 23, 14, 54, 11, 187, DateTimeKind.Utc).AddTicks(4310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 139, DateTimeKind.Utc).AddTicks(6899));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 23, 14, 54, 11, 157, DateTimeKind.Utc).AddTicks(1228),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 67, DateTimeKind.Utc).AddTicks(1777));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 14, 54, 11, 168, DateTimeKind.Utc).AddTicks(8653));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 14, 54, 11, 187, DateTimeKind.Utc).AddTicks(5993));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 14, 54, 11, 187, DateTimeKind.Utc).AddTicks(7803));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 14, 54, 11, 187, DateTimeKind.Utc).AddTicks(7805));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 23, 14, 54, 11, 187, DateTimeKind.Utc).AddTicks(7807));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplyRelationIsConfirmed",
                table: "SupplyRelations");

            migrationBuilder.DropColumn(
                name: "ServiceIsConfirmed",
                table: "Services");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 22, 9, 38, 51, 153, DateTimeKind.Utc).AddTicks(5592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 23, 15, 4, 11, 196, DateTimeKind.Utc).AddTicks(9876));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 139, DateTimeKind.Utc).AddTicks(6899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 23, 14, 54, 11, 187, DateTimeKind.Utc).AddTicks(4310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 67, DateTimeKind.Utc).AddTicks(1777),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 23, 14, 54, 11, 157, DateTimeKind.Utc).AddTicks(1228));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 9, 28, 51, 92, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 9, 28, 51, 141, DateTimeKind.Utc).AddTicks(327));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 9, 28, 51, 141, DateTimeKind.Utc).AddTicks(9221));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 9, 28, 51, 141, DateTimeKind.Utc).AddTicks(9227));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 9, 28, 51, 141, DateTimeKind.Utc).AddTicks(9229));
        }
    }
}
