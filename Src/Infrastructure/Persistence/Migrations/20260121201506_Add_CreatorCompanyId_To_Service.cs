using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatorCompanyId_To_Service : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 21, 20, 25, 4, 699, DateTimeKind.Utc).AddTicks(7303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 2, 14, 13, 22, 209, DateTimeKind.Utc).AddTicks(1317));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorCompanyId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 685, DateTimeKind.Utc).AddTicks(7754),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 2, 14, 3, 22, 200, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 650, DateTimeKind.Utc).AddTicks(7829),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 2, 14, 3, 22, 179, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 20, 15, 4, 656, DateTimeKind.Utc).AddTicks(2741));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 20, 15, 4, 685, DateTimeKind.Utc).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 20, 15, 4, 686, DateTimeKind.Utc).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 20, 15, 4, 686, DateTimeKind.Utc).AddTicks(662));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 20, 15, 4, 686, DateTimeKind.Utc).AddTicks(676));

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatorCompanyId",
                table: "Services",
                column: "CreatorCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Services_CreatorCompanyId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatorCompanyId",
                table: "Services");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 2, 14, 13, 22, 209, DateTimeKind.Utc).AddTicks(1317),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 21, 20, 25, 4, 699, DateTimeKind.Utc).AddTicks(7303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 2, 14, 3, 22, 200, DateTimeKind.Utc).AddTicks(8091),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 685, DateTimeKind.Utc).AddTicks(7754));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 2, 14, 3, 22, 179, DateTimeKind.Utc).AddTicks(849),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 650, DateTimeKind.Utc).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 2, 14, 3, 22, 184, DateTimeKind.Utc).AddTicks(3439));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 2, 14, 3, 22, 200, DateTimeKind.Utc).AddTicks(9827));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 2, 14, 3, 22, 201, DateTimeKind.Utc).AddTicks(1379));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 2, 14, 3, 22, 201, DateTimeKind.Utc).AddTicks(1382));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 2, 14, 3, 22, 201, DateTimeKind.Utc).AddTicks(1384));
        }
    }
}
