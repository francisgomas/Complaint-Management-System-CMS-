using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class UpdateModelNursingStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "NursingStation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 11, 21, 18, 109, DateTimeKind.Local).AddTicks(8159));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 11, 21, 18, 109, DateTimeKind.Local).AddTicks(8162));

            migrationBuilder.CreateIndex(
                name: "IX_NursingStation_StatusId",
                table: "NursingStation",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_NursingStation_Status_StatusId",
                table: "NursingStation",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NursingStation_Status_StatusId",
                table: "NursingStation");

            migrationBuilder.DropIndex(
                name: "IX_NursingStation_StatusId",
                table: "NursingStation");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "NursingStation");

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
        }
    }
}
