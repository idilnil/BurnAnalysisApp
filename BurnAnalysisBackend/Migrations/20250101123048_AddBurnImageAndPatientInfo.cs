using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BurnAnalysisApp.Migrations
{
    /// <inheritdoc />
    public partial class AddBurnImageAndPatientInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    MedicalHistory = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "BurnImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientID = table.Column<int>(type: "integer", nullable: false),
                    UploadedBy = table.Column<int>(type: "integer", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BurnDepth = table.Column<string>(type: "text", nullable: true),
                    SurfaceArea = table.Column<float>(type: "real", nullable: true),
                    BodyLocation = table.Column<string>(type: "text", nullable: true),
                    SeverityScore = table.Column<float>(type: "real", nullable: true),
                    AnalysisDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SharedWithDoctors = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurnImages", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_BurnImages_Doctors_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BurnImages_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BurnImages_PatientID",
                table: "BurnImages",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_BurnImages_UploadedBy",
                table: "BurnImages",
                column: "UploadedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurnImages");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
