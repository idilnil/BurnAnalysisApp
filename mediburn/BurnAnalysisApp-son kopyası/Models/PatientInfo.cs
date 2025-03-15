using System.ComponentModel.DataAnnotations;

namespace BurnAnalysisApp.Models
{
    public class PatientInfo
    {
        [Key]
        public int PatientID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, 120)] // Yaş 0 ile 120 arasında olmalı
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        public string? MedicalHistory { get; set; }

        // BurnFormInfo tablosundan alınan alanlar
        public string? BurnCause { get; set; }
        public DateTime? HospitalArrivalDate { get; set; }
        public DateTime? BurnOccurrenceDate { get; set; }
        public string? PhotoPath { get; set; }
        public string? BurnDepth { get; set; } 

    }
}
