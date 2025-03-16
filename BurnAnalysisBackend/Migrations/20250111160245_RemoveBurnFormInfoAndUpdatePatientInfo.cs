using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BurnAnalysisApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBurnFormInfoAndUpdatePatientInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BurnCause",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BurnOccurrenceDate",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HospitalArrivalDate",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BurnFormInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientName = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    BurnCause = table.Column<string>(type: "text", nullable: true),
                    HospitalArrivalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BurnOccurrenceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhotoPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurnFormInfo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurnFormInfo");

            migrationBuilder.DropColumn(
                name: "BurnCause",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BurnOccurrenceDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HospitalArrivalDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Patients");
        }
    }
}
