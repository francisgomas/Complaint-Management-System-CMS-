using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class AddFieldsToComplaintForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthCenterId",
                table: "ComplaintDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "ComplaintDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NursingStationId",
                table: "ComplaintDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 11, 13, 15, 33, 21, 596, DateTimeKind.Local).AddTicks(5588));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 11, 13, 15, 33, 21, 596, DateTimeKind.Local).AddTicks(5592));

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintDetails_HealthCenterId",
                table: "ComplaintDetails",
                column: "HealthCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintDetails_HospitalId",
                table: "ComplaintDetails",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintDetails_NursingStationId",
                table: "ComplaintDetails",
                column: "NursingStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintDetails_HealthCenter_HealthCenterId",
                table: "ComplaintDetails",
                column: "HealthCenterId",
                principalTable: "HealthCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintDetails_Hospital_HospitalId",
                table: "ComplaintDetails",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintDetails_NursingStation_NursingStationId",
                table: "ComplaintDetails",
                column: "NursingStationId",
                principalTable: "NursingStation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintDetails_HealthCenter_HealthCenterId",
                table: "ComplaintDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintDetails_Hospital_HospitalId",
                table: "ComplaintDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintDetails_NursingStation_NursingStationId",
                table: "ComplaintDetails");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintDetails_HealthCenterId",
                table: "ComplaintDetails");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintDetails_HospitalId",
                table: "ComplaintDetails");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintDetails_NursingStationId",
                table: "ComplaintDetails");

            migrationBuilder.DropColumn(
                name: "HealthCenterId",
                table: "ComplaintDetails");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "ComplaintDetails");

            migrationBuilder.DropColumn(
                name: "NursingStationId",
                table: "ComplaintDetails");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 11, 13, 1, 6, 1, 154, DateTimeKind.Local).AddTicks(4832));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 11, 13, 1, 6, 1, 154, DateTimeKind.Local).AddTicks(4834));
        }
    }
}
