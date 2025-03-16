namespace BurnAnalysisApp.Models;
    public class BurnFormInfo
    {
        public int Id { get; set; }
        public string ?PatientName { get; set; }
        public int Age { get; set; }
        public string ?Gender { get; set; }
        public string ?BurnCause { get; set; }
        public DateTime HospitalArrivalDate { get; set; }
        public DateTime BurnOccurrenceDate { get; set; }

         public string? PhotoPath { get; set; }
    }


