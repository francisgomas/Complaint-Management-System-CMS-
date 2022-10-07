using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class UpdateModelHealthCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "HealthCenter",
                newName: "StatusId");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 10, 56, 1, 582, DateTimeKind.Local).AddTicks(1675));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 10, 56, 1, 582, DateTimeKind.Local).AddTicks(1678));

            migrationBuilder.CreateIndex(
                name: "IX_HealthCenter_StatusId",
                table: "HealthCenter",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthCenter_Status_StatusId",
                table: "HealthCenter",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthCenter_Status_StatusId",
                table: "HealthCenter");

            migrationBuilder.DropIndex(
                name: "IX_HealthCenter_StatusId",
                table: "HealthCenter");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "HealthCenter",
                newName: "Status");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 10, 19, 12, 573, DateTimeKind.Local).AddTicks(2495));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 10, 19, 12, 573, DateTimeKind.Local).AddTicks(2498));
        }
    }
}
