using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurnAnalysisApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVisitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Patients_PatientID",
                table: "Visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visit",
                table: "Visit");

            migrationBuilder.RenameTable(
                name: "Visit",
                newName: "Visits");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_PatientID",
                table: "Visits",
                newName: "IX_Visits_PatientID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visits",
                table: "Visits",
                column: "VisitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Patients_PatientID",
                table: "Visits",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Patients_PatientID",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visits",
                table: "Visits");

            migrationBuilder.RenameTable(
                name: "Visits",
                newName: "Visit");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_PatientID",
                table: "Visit",
                newName: "IX_Visit_PatientID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visit",
                table: "Visit",
                column: "VisitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Patients_PatientID",
                table: "Visit",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
