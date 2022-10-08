using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    public partial class AddComplaintDetailsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplaintId",
                table: "ComplaintForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplaintDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateofIncident = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: true),
                    ComplaintReasonId = table.Column<int>(type: "int", nullable: false),
                    ComplaintFirstTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintBehalf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintFirstTimeReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ComplaintBehalfReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ComplaintExplanation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Remedy = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintDetails_ComplaintReason_ComplaintReasonId",
                        column: x => x.ComplaintReasonId,
                        principalTable: "ComplaintReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintDetails_HealthFacility_HealthFacilityId",
                        column: x => x.HealthFacilityId,
                        principalTable: "HealthFacility",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 8, 17, 45, 8, 852, DateTimeKind.Local).AddTicks(5912));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 10, 8, 17, 45, 8, 852, DateTimeKind.Local).AddTicks(5913));

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintForms_ComplaintId",
                table: "ComplaintForms",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintDetails_ComplaintReasonId",
                table: "ComplaintDetails",
                column: "ComplaintReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintDetails_HealthFacilityId",
                table: "ComplaintDetails",
                column: "HealthFacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintForms_ComplaintDetails_ComplaintId",
                table: "ComplaintForms",
                column: "ComplaintId",
                principalTable: "ComplaintDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintForms_ComplaintDetails_ComplaintId",
                table: "ComplaintForms");

            migrationBuilder.DropTable(
                name: "ComplaintDetails");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintForms_ComplaintId",
                table: "ComplaintForms");

            migrationBuilder.DropColumn(
                name: "ComplaintId",
                table: "ComplaintForms");

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
        }
    }
}
