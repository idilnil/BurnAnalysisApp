using Microsoft.AspNetCore.Mvc;
using BurnAnalysisApp.Data;
using BurnAnalysisApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnAnalysisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Patient/search?searchTerm=John
        [HttpGet("search")]
        public async Task<IActionResult> SearchPatients(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Arama terimi boş olamaz.");

            var patients = await _context.Patients
                .Where(p => p.Name.Contains(searchTerm))
                .ToListAsync();

            return Ok(patients);
        }

        // GET: api/Patient/{id} - Tek bir hasta için bilgi al
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return NotFound("Hasta bulunamadı.");

            return Ok(patient);
        }

        // GET: api/Patient - Tüm hastaları getir
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            return Ok(patients);
        }

        // POST: api/Patient - Yeni hasta ekle
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromForm] PatientInfo patient, IFormFile photo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (photo != null)
            {
                var photoPath = Path.Combine("Uploads", Guid.NewGuid() + Path.GetExtension(photo.FileName));
                using (var stream = new FileStream(photoPath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                patient.PhotoPath = photoPath;
            }

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatients), new { id = patient.PatientID }, patient);
        }

        // PUT: api/Patient/{id} - Hasta bilgilerini güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientInfo patient)
        {
            if (id != patient.PatientID)
                return BadRequest("ID uyuşmazlığı.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPatient = await _context.Patients.FindAsync(id);
            if (existingPatient == null)
                return NotFound("Hasta bulunamadı.");

            existingPatient.Name = patient.Name;
            existingPatient.Age = patient.Age;
            existingPatient.Gender = patient.Gender;
            existingPatient.MedicalHistory = patient.MedicalHistory;
            existingPatient.BurnCause = patient.BurnCause;
            existingPatient.HospitalArrivalDate = patient.HospitalArrivalDate;
            existingPatient.BurnOccurrenceDate = patient.BurnOccurrenceDate;

            _context.Patients.Update(existingPatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Patient/{id}/burndepth - Yanık derinliği güncelle
        [HttpPut("{id}/burndepth")]
        public async Task<IActionResult> UpdateBurnDepth(int id, [FromBody] string burnDepth)
        {
            var existingPatient = await _context.Patients.FindAsync(id);
            if (existingPatient == null)
                return NotFound("Hasta bulunamadı.");

            existingPatient.BurnDepth = burnDepth;
            _context.Patients.Update(existingPatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Patient/{id} - Hasta sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return NotFound("Hasta bulunamadı.");

            if (!string.IsNullOrEmpty(patient.PhotoPath) && System.IO.File.Exists(patient.PhotoPath))
            {
                System.IO.File.Delete(patient.PhotoPath);
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
