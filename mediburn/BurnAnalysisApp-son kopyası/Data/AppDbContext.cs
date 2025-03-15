using BurnAnalysisApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnAnalysisApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<BurnImage> BurnImages { get; set; }
        public DbSet<PatientInfo> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-Many relationship between Admin and Doctor
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Admin)
                .WithMany()
                .HasForeignKey(d => d.AdminID)
                .OnDelete(DeleteBehavior.SetNull);

            // One-to-Many relationship between Patient and BurnImage
            modelBuilder.Entity<BurnImage>()
                .HasOne(b => b.Patient)
                .WithMany()
                .HasForeignKey(b => b.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many relationship between Doctor and BurnImage
            modelBuilder.Entity<BurnImage>()
                .HasOne(b => b.Doctor)
                .WithMany()
                .HasForeignKey(b => b.UploadedBy)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
