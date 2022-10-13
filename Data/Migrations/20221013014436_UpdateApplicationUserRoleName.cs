using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class UpdateApplicationUserRoleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 13, 44, 35, 943, DateTimeKind.Local).AddTicks(7092));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 13, 44, 35, 943, DateTimeKind.Local).AddTicks(7097));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 8, 29, 38, 767, DateTimeKind.Local).AddTicks(8043));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 8, 29, 38, 767, DateTimeKind.Local).AddTicks(8050));
        }
    }
}
