﻿// <auto-generated />
using System;
using BurnAnalysisApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BurnAnalysisApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
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

            modelBuilder.Entity("BurnAnalysisApp.Models.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("NotificationID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DoctorID")
                        .HasColumnType("integer");

                    b.Property<int?>("ForumPostID")
                        .HasColumnType("integer");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("NotificationID");

                    b.HasIndex("ForumPostID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.PatientInfo", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PatientID"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("AudioPath")
                        .HasColumnType("text");

                    b.Property<string>("BurnArea")
                        .HasColumnType("text");

                    b.Property<string>("BurnCause")
                        .HasColumnType("text");

                    b.Property<string>("BurnDepth")
                        .HasColumnType("text");

                    b.Property<DateTime?>("BurnOccurrenceDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

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

                    b.Property<bool>("ReminderSent")
                        .HasColumnType("boolean");

                    b.HasKey("PatientID");

                    b.HasIndex("PatientID")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.Visit", b =>
                {
                    b.Property<int>("VisitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VisitID"));

                    b.Property<string>("LabResultsFilePath")
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("PatientID")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("text");

                    b.Property<string>("PrescribedMedications")
                        .HasColumnType("text");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("VisitID");

                    b.HasIndex("PatientID");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.VoiceRecording", b =>
                {
                    b.Property<int>("VoiceRecordingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VoiceRecordingID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ForumPostID")
                        .HasColumnType("integer");

                    b.HasKey("VoiceRecordingID");

                    b.HasIndex("ForumPostID");

                    b.ToTable("VoiceRecordings");
                });

            modelBuilder.Entity("Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CommentID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ForumPostID")
                        .HasColumnType("integer");

                    b.HasKey("CommentID");

                    b.HasIndex("ForumPostID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ForumPost", b =>
                {
                    b.Property<int>("ForumPostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ForumPostID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PatientID")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("text");

                    b.HasKey("ForumPostID");

                    b.HasIndex("PatientID");

                    b.ToTable("ForumPosts");
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

            modelBuilder.Entity("BurnAnalysisApp.Models.Notification", b =>
                {
                    b.HasOne("ForumPost", "ForumPost")
                        .WithMany("Notifications")
                        .HasForeignKey("ForumPostID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ForumPost");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.Visit", b =>
                {
                    b.HasOne("BurnAnalysisApp.Models.PatientInfo", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("BurnAnalysisApp.Models.VoiceRecording", b =>
                {
                    b.HasOne("ForumPost", "ForumPost")
                        .WithMany("VoiceRecordings")
                        .HasForeignKey("ForumPostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForumPost");
                });

            modelBuilder.Entity("Comment", b =>
                {
                    b.HasOne("ForumPost", null)
                        .WithMany("Comments")
                        .HasForeignKey("ForumPostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ForumPost", b =>
                {
                    b.HasOne("BurnAnalysisApp.Models.PatientInfo", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ForumPost", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Notifications");

                    b.Navigation("VoiceRecordings");
                });
#pragma warning restore 612, 618
        }
    }
}
