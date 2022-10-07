using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class AddFormStatusModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplainantId",
                table: "ComplaintForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplainantDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ResidentialAddr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalAddr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TownCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainantDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplainantDetails_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormStatus", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintForms_ComplainantId",
                table: "ComplaintForms",
                column: "ComplainantId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplainantDetails_CountryId",
                table: "ComplainantDetails",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintForms_ComplainantDetails_ComplainantId",
                table: "ComplaintForms",
                column: "ComplainantId",
                principalTable: "ComplainantDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintForms_ComplainantDetails_ComplainantId",
                table: "ComplaintForms");

            migrationBuilder.DropTable(
                name: "ComplainantDetails");

            migrationBuilder.DropTable(
                name: "FormStatus");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintForms_ComplainantId",
                table: "ComplaintForms");

            migrationBuilder.DropColumn(
                name: "ComplainantId",
                table: "ComplaintForms");

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
        }
    }
}
