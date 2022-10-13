using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class RemoveAppUserFromNotificationsToComplaintForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_AssignedToId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_AssignedToId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId",
                table: "ComplaintForms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 20, 30, 13, 272, DateTimeKind.Local).AddTicks(4752));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 20, 30, 13, 272, DateTimeKind.Local).AddTicks(4754));

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintForms_AssignedToId",
                table: "ComplaintForms",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintForms_AspNetUsers_AssignedToId",
                table: "ComplaintForms",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintForms_AspNetUsers_AssignedToId",
                table: "ComplaintForms");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintForms_AssignedToId",
                table: "ComplaintForms");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "ComplaintForms");

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 18, 31, 26, 554, DateTimeKind.Local).AddTicks(8955));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 13, 18, 31, 26, 554, DateTimeKind.Local).AddTicks(8956));

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AssignedToId",
                table: "Notification",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_AssignedToId",
                table: "Notification",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
