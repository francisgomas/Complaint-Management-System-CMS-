using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class UpdateCountryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nicename",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Numcode",
                table: "Country");

            migrationBuilder.AlterColumn<int>(
                name: "Phonecode",
                table: "Country",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "ComplaintForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 20, 23, 3, 834, DateTimeKind.Local).AddTicks(5223));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 20, 23, 3, 834, DateTimeKind.Local).AddTicks(5225));

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintForms_StatusId",
                table: "ComplaintForms",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintForms_Status_StatusId",
                table: "ComplaintForms",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintForms_Status_StatusId",
                table: "ComplaintForms");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintForms_StatusId",
                table: "ComplaintForms");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "ComplaintForms");

            migrationBuilder.AlterColumn<string>(
                name: "Phonecode",
                table: "Country",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Nicename",
                table: "Country",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Numcode",
                table: "Country",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 20, 16, 3, 365, DateTimeKind.Local).AddTicks(9945));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 20, 16, 3, 365, DateTimeKind.Local).AddTicks(9948));
        }
    }
}
