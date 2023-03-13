using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diseas_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "BodyParts",
                columns: table => new
                {
                    bodypartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bodypartName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyParts", x => x.bodypartId);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    diseasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diseasName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    symptoms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ageRangeStart = table.Column<int>(type: "int", nullable: false),
                    ageRangeEnd = table.Column<int>(type: "int", nullable: false),
                    discoveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    infectionRate = table.Column<float>(type: "real", nullable: false),
                    deathRate = table.Column<float>(type: "real", nullable: false),
                    spreadingWays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeOfInfection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeOfDiseas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isSelfCurable = table.Column<bool>(type: "bit", nullable: false),
                    vaccineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recoveryTime = table.Column<int>(type: "int", nullable: false),
                    diseasSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    precautions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.diseasId);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    medicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    medicineName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.medicineId);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    reportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reportName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.reportId);
                });

            migrationBuilder.CreateTable(
                name: "DiseasBodyParts",
                columns: table => new
                {
                    diseasBodyPartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseasId = table.Column<int>(type: "int", nullable: true),
                    bodypartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseasBodyParts", x => x.diseasBodyPartId);
                    table.ForeignKey(
                        name: "FK_DiseasBodyParts_BodyParts_bodypartId",
                        column: x => x.bodypartId,
                        principalTable: "BodyParts",
                        principalColumn: "bodypartId");
                    table.ForeignKey(
                        name: "FK_DiseasBodyParts_Diseases_diseasId",
                        column: x => x.diseasId,
                        principalTable: "Diseases",
                        principalColumn: "diseasId");
                });

            migrationBuilder.CreateTable(
                name: "DiseasMedicines",
                columns: table => new
                {
                    diseasMedicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseasId = table.Column<int>(type: "int", nullable: true),
                    medicineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseasMedicines", x => x.diseasMedicineId);
                    table.ForeignKey(
                        name: "FK_DiseasMedicines_Diseases_diseasId",
                        column: x => x.diseasId,
                        principalTable: "Diseases",
                        principalColumn: "diseasId");
                    table.ForeignKey(
                        name: "FK_DiseasMedicines_Medicines_medicineId",
                        column: x => x.medicineId,
                        principalTable: "Medicines",
                        principalColumn: "medicineId");
                });

            migrationBuilder.CreateTable(
                name: "DiseasReports",
                columns: table => new
                {
                    diseasReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseasId = table.Column<int>(type: "int", nullable: true),
                    reportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseasReports", x => x.diseasReportId);
                    table.ForeignKey(
                        name: "FK_DiseasReports_Diseases_diseasId",
                        column: x => x.diseasId,
                        principalTable: "Diseases",
                        principalColumn: "diseasId");
                    table.ForeignKey(
                        name: "FK_DiseasReports_Reports_reportId",
                        column: x => x.reportId,
                        principalTable: "Reports",
                        principalColumn: "reportId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiseasBodyParts_bodypartId",
                table: "DiseasBodyParts",
                column: "bodypartId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasBodyParts_diseasId",
                table: "DiseasBodyParts",
                column: "diseasId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasMedicines_diseasId",
                table: "DiseasMedicines",
                column: "diseasId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasMedicines_medicineId",
                table: "DiseasMedicines",
                column: "medicineId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasReports_diseasId",
                table: "DiseasReports",
                column: "diseasId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseasReports_reportId",
                table: "DiseasReports",
                column: "reportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "DiseasBodyParts");

            migrationBuilder.DropTable(
                name: "DiseasMedicines");

            migrationBuilder.DropTable(
                name: "DiseasReports");

            migrationBuilder.DropTable(
                name: "BodyParts");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
