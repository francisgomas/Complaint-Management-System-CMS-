using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class UpdateModelHospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Hospital",
                newName: "StatusId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_StatusId",
                table: "Hospital",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_Status_StatusId",
                table: "Hospital",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_Status_StatusId",
                table: "Hospital");

            migrationBuilder.DropIndex(
                name: "IX_Hospital_StatusId",
                table: "Hospital");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Hospital",
                newName: "Status");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 9, 58, 3, 212, DateTimeKind.Local).AddTicks(14));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 9, 58, 3, 212, DateTimeKind.Local).AddTicks(16));
        }
    }
}
