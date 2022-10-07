using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class AddStatusToHealthFacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "HealthFacility",
                newName: "StatusId");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 5, 23, 23, 7, 650, DateTimeKind.Local).AddTicks(1622));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 5, 23, 23, 7, 650, DateTimeKind.Local).AddTicks(1624));

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacility_StatusId",
                table: "HealthFacility",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacility_Status_StatusId",
                table: "HealthFacility",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacility_Status_StatusId",
                table: "HealthFacility");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacility_StatusId",
                table: "HealthFacility");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "HealthFacility",
                newName: "Status");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 5, 22, 32, 10, 50, DateTimeKind.Local).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 5, 22, 32, 10, 50, DateTimeKind.Local).AddTicks(2831));
        }
    }
}
