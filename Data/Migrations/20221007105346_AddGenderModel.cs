using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class AddGenderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintForms_Status_StatusId",
                table: "ComplaintForms");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ComplainantDetails");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ComplaintForms",
                newName: "FormStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintForms_StatusId",
                table: "ComplaintForms",
                newName: "IX_ComplaintForms_FormStatusId");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "ComplainantDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 22, 53, 46, 500, DateTimeKind.Local).AddTicks(7492));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 22, 53, 46, 500, DateTimeKind.Local).AddTicks(7494));

            migrationBuilder.CreateIndex(
                name: "IX_ComplainantDetails_GenderId",
                table: "ComplainantDetails",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplainantDetails_Gender_GenderId",
                table: "ComplainantDetails",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintForms_FormStatus_FormStatusId",
                table: "ComplaintForms",
                column: "FormStatusId",
                principalTable: "FormStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplainantDetails_Gender_GenderId",
                table: "ComplainantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintForms_FormStatus_FormStatusId",
                table: "ComplaintForms");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropIndex(
                name: "IX_ComplainantDetails_GenderId",
                table: "ComplainantDetails");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "ComplainantDetails");

            migrationBuilder.RenameColumn(
                name: "FormStatusId",
                table: "ComplaintForms",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintForms_FormStatusId",
                table: "ComplaintForms",
                newName: "IX_ComplaintForms_StatusId");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "ComplainantDetails",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 21, 27, 22, 476, DateTimeKind.Local).AddTicks(1901));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 7, 21, 27, 22, 476, DateTimeKind.Local).AddTicks(1905));

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintForms_Status_StatusId",
                table: "ComplaintForms",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
