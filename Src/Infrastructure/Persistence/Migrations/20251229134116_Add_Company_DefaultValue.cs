using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Company_DefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 29, 13, 51, 16, 99, DateTimeKind.Utc).AddTicks(9073),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 28, 19, 33, 22, 12, DateTimeKind.Utc).AddTicks(9521));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 29, 13, 41, 16, 92, DateTimeKind.Utc).AddTicks(4933),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(3323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 29, 13, 41, 16, 69, DateTimeKind.Utc).AddTicks(4713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 28, 19, 23, 21, 983, DateTimeKind.Utc).AddTicks(4989));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "CompanyName", "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"), "DefaultCompany", new DateTime(2025, 12, 29, 13, 41, 16, 75, DateTimeKind.Utc).AddTicks(104), null, false, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("a1f5c9d2-3b4e-4f7a-9c2d-8e1b7f6a9d11"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 29, 13, 41, 16, 92, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b2e6d3a4-5c7f-4a8b-9d3e-7f2c8a6b5e22"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 29, 13, 41, 16, 92, DateTimeKind.Utc).AddTicks(9654));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c3f7e4b5-6d8a-4b9c-8e4f-6a3d9b7c4f33"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 29, 13, 41, 16, 92, DateTimeKind.Utc).AddTicks(9658));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("d4a8f5c6-7e9b-4c0d-9f5a-5b4e8c7d6a44"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 29, 13, 41, 16, 92, DateTimeKind.Utc).AddTicks(9661));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3f8c2d9a-7b41-4f9d-9d2a-8a6f3b2c1e45"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 28, 19, 33, 22, 12, DateTimeKind.Utc).AddTicks(9521),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 29, 13, 51, 16, 99, DateTimeKind.Utc).AddTicks(9073));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 28, 19, 23, 22, 5, DateTimeKind.Utc).AddTicks(3323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 29, 13, 41, 16, 92, DateTimeKind.Utc).AddTicks(4933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 28, 19, 23, 21, 983, DateTimeKind.Utc).AddTicks(4989),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 12, 29, 13, 41, 16, 69, DateTimeKind.Utc).AddTicks(4713));

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
        }
    }
}
