using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Relation_Between_Company_And_Service : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 22, 9, 38, 51, 153, DateTimeKind.Utc).AddTicks(5592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 21, 20, 25, 4, 699, DateTimeKind.Utc).AddTicks(7303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 139, DateTimeKind.Utc).AddTicks(6899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 685, DateTimeKind.Utc).AddTicks(7754));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 67, DateTimeKind.Utc).AddTicks(1777),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 650, DateTimeKind.Utc).AddTicks(7829));

            migrationBuilder.CreateTable(
                name: "CompanyService",
                columns: table => new
                {
                    ServicesServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierCompaniesCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyService", x => new { x.ServicesServiceId, x.SupplierCompaniesCompanyId });
                    table.ForeignKey(
                        name: "FK_CompanyService_Companies_SupplierCompaniesCompanyId",
                        column: x => x.SupplierCompaniesCompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyService_Services_ServicesServiceId",
                        column: x => x.ServicesServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CompanyService_SupplierCompaniesCompanyId",
                table: "CompanyService",
                column: "SupplierCompaniesCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyService");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserTokenExpireTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 21, 20, 25, 4, 699, DateTimeKind.Utc).AddTicks(7303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 22, 9, 38, 51, 153, DateTimeKind.Utc).AddTicks(5592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 685, DateTimeKind.Utc).AddTicks(7754),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 139, DateTimeKind.Utc).AddTicks(6899));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionAtTime",
                table: "Audits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 1, 21, 20, 15, 4, 650, DateTimeKind.Utc).AddTicks(7829),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 1, 22, 9, 28, 51, 67, DateTimeKind.Utc).AddTicks(1777));

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
        }
    }
}
