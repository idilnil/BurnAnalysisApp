﻿// <auto-generated />
using System;
using BurnAnalysisApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BurnAnalysisApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250115100530_UpdatePatientInfoWithBurnDetails")]
    partial class UpdatePatientInfoWithBurnDetails
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BurnAnalysisApp.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdminID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.BurnImage", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ImageID"));

                    b.Property<DateTime?>("AnalysisDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BodyLocation")
                        .HasColumnType("text");

                    b.Property<string>("BurnDepth")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PatientID")
                        .HasColumnType("integer");

                    b.Property<float?>("SeverityScore")
                        .HasColumnType("real");

                    b.Property<bool>("SharedWithDoctors")
                        .HasColumnType("boolean");

                    b.Property<float?>("SurfaceArea")
                        .HasColumnType("real");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UploadedBy")
                        .HasColumnType("integer");

                    b.HasKey("ImageID");

                    b.HasIndex("PatientID");

                    b.HasIndex("UploadedBy");

                    b.ToTable("BurnImages");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DoctorID"));

                    b.Property<int?>("AdminID")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean");

                    b.HasKey("DoctorID");

                    b.HasIndex("AdminID");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.PatientInfo", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PatientID"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("BurnCause")
                        .HasColumnType("text");

                    b.Property<string>("BurnDepth")
                        .HasColumnType("text");

                    b.Property<DateTime?>("BurnOccurrenceDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("HospitalArrivalDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MedicalHistory")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("text");

                    b.HasKey("PatientID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.BurnImage", b =>
                {
                    b.HasOne("BurnAnalysisApp.Models.PatientInfo", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BurnAnalysisApp.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("UploadedBy")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.Doctor", b =>
                {
                    b.HasOne("BurnAnalysisApp.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Admin");
                });
#pragma warning restore 612, 618
        }
    }
}
