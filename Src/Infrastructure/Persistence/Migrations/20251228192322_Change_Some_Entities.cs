using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_Some_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Companies_SupplierCompanyId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_SupplierCompanyId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SupplierCompanyId",
                table: "Services");

            migrationBuilder.AlterColumn<bool>(
                name: "UserTokenIsUsed",
                table: "UserTokens",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "UserTokenIsExpired",
                table: "UserTokens",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 28, 19, 33, 22, 12, DateTimeKind.Utc).AddTicks(9521),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 25, 17, 10, 46, 495, DateTimeKind.Utc).AddTicks(5917));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserCompanyId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierCompanyId",
                table: "SupplyRelations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "ServiceIsActive",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(3323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 25, 17, 0, 46, 487, DateTimeKind.Utc).AddTicks(9768));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 28, 19, 23, 21, 983, DateTimeKind.Utc).AddTicks(4989),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(5450));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(7555));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(7558));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(7559));

            migrationBuilder.CreateIndex(
                name: "IX_SupplyRelations_SupplierCompanyId",
                table: "SupplyRelations",
                column: "SupplierCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyRelations_Companies_SupplierCompanyId",
                table: "SupplyRelations",
                column: "SupplierCompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplyRelations_Companies_SupplierCompanyId",
                table: "SupplyRelations");

            migrationBuilder.DropIndex(
                name: "IX_SupplyRelations_SupplierCompanyId",
                table: "SupplyRelations");

            migrationBuilder.DropColumn(
                name: "SupplierCompanyId",
                table: "SupplyRelations");

            migrationBuilder.AlterColumn<bool>(
                name: "UserTokenIsUsed",
                table: "UserTokens",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "UserTokenIsExpired",
                table: "UserTokens",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 25, 17, 10, 46, 495, DateTimeKind.Utc).AddTicks(5917),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 28, 19, 33, 22, 12, DateTimeKind.Utc).AddTicks(9521));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserCompanyId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"));

            migrationBuilder.AlterColumn<bool>(
                name: "ServiceIsActive",
                table: "Services",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierCompanyId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 25, 17, 0, 46, 487, DateTimeKind.Utc).AddTicks(9768),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(3323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 28, 19, 23, 21, 983, DateTimeKind.Utc).AddTicks(4989));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 17, 0, 46, 488, DateTimeKind.Utc).AddTicks(2521));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 17, 0, 46, 488, DateTimeKind.Utc).AddTicks(4518));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 17, 0, 46, 488, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 17, 0, 46, 488, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.CreateIndex(
                name: "IX_Services_SupplierCompanyId",
                table: "Services",
                column: "SupplierCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Companies_SupplierCompanyId",
                table: "Services",
                column: "SupplierCompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");
        }
    }
}
