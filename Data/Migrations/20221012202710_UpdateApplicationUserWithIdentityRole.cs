using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class UpdateApplicationUserWithIdentityRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 8, 27, 9, 918, DateTimeKind.Local).AddTicks(5499));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 8, 27, 9, 918, DateTimeKind.Local).AddTicks(5501));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityRoleId",
                table: "AspNetUsers",
                column: "IdentityRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_IdentityRoleId",
                table: "AspNetUsers",
                column: "IdentityRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_IdentityRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 8, 10, 42, 186, DateTimeKind.Local).AddTicks(8899));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 8, 10, 42, 186, DateTimeKind.Local).AddTicks(8901));
        }
    }
}
