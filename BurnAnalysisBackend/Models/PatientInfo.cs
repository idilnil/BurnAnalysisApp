using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurnAnalysisApp.Models
{
    public class PatientInfo
    {
        [Key]
        public int PatientID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        public string? MedicalHistory { get; set; }

        // Yanık bilgileri
        public string? BurnCause { get; set; }
        public DateTime? HospitalArrivalDate { get; set; }
        public DateTime? BurnOccurrenceDate { get; set; }
        public string? PhotoPath { get; set; }
        public string? BurnDepth { get; set; }

        // Bir hastanın birden fazla ziyareti olabilir (One-to-Many ilişkisi)
        public ICollection<Visit>? Visits { get; set; }
    }
}
