using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class UpdateModelComplaintReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ComplaintReason",
                newName: "StatusId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintReason_StatusId",
                table: "ComplaintReason",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintReason_Status_StatusId",
                table: "ComplaintReason",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintReason_Status_StatusId",
                table: "ComplaintReason");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintReason_StatusId",
                table: "ComplaintReason");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ComplaintReason",
                newName: "Status");

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
        }
    }
}
