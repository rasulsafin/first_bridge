using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DM.DAL.Migrations
{
    public partial class UpdatedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(3789));

            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(4165));

            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(4184));

            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(4501));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(154));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(173));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(187));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(199));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "ListField",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(8611));

            migrationBuilder.UpdateData(
                table: "ListField",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(9023));

            migrationBuilder.UpdateData(
                table: "ListField",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(9356));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1901));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1914));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1927));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1943));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1955));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1883));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(1968));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 669, DateTimeKind.Local).AddTicks(6929));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "Record",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(6662));

            migrationBuilder.UpdateData(
                table: "Record",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(6796));

            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(7608));

            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 670, DateTimeKind.Local).AddTicks(7743));

            migrationBuilder.UpdateData(
                table: "UserProject",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 59, 671, DateTimeKind.Local).AddTicks(2665));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(8478));

            migrationBuilder.UpdateData(
                table: "Field",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(2744));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(3079));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(3098));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(3113));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(3126));

            migrationBuilder.UpdateData(
                table: "List",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(3143));

            migrationBuilder.UpdateData(
                table: "ListField",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(1378));

            migrationBuilder.UpdateData(
                table: "ListField",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(1794));

            migrationBuilder.UpdateData(
                table: "ListField",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(2250));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5348));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5534));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5547));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5561));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5576));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5590));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(5603));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 674, DateTimeKind.Local).AddTicks(455));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 674, DateTimeKind.Local).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "Record",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 674, DateTimeKind.Local).AddTicks(9313));

            migrationBuilder.UpdateData(
                table: "Record",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 674, DateTimeKind.Local).AddTicks(9452));

            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(235));

            migrationBuilder.UpdateData(
                table: "Template",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(359));

            migrationBuilder.UpdateData(
                table: "UserProject",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 5, 5, 9, 23, 9, 675, DateTimeKind.Local).AddTicks(6343));
        }
    }
}
