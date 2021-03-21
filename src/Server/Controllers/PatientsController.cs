using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedMan.API.DTOs;

namespace MedMan.API.Controllers
{
    public class PatientsController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            var patList = await _context.Patients.ToListAsync();

            var dtoList = new List<PatientDto>();

            foreach(var pat in patList)
            {
                dtoList.Add(new PatientDto
                {
                    Id = pat.Id,
                    FamilyName = pat.familyName,
                    GivenName = pat.firstName,
                    DOB = pat.DOB
                });
            }

            return dtoList;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            var admins = new List<AdministrationDto>();
            var scripts = new List<PrescriptionDto>();

            foreach(var admin in patient.administrations)
            {
                admins.Add(new AdministrationDto
                {
                    Id = admin.Id,
                    Dose = admin.dose,
                    Medication = new MedicationDto
                    {
                        Id = admin.medicationId,
                        Name = admin.medication.name
                    },
                    TimeGiven = admin.timeGiven
                });
            }

            foreach(var script in patient.prescriptions)
            {
                scripts.Add(new PrescriptionDto
                {
                    Id = script.Id,
                    Dose = script.dose,
                    Medication = new MedicationDto
                    {
                        Id = script.medicationId,
                        Name = script.medication.name
                    }
                });
            }

            var dto = new PatientDto
            {
                Id = patient.Id,
                GivenName = patient.firstName,
                FamilyName = patient.familyName,
                DOB = patient.DOB,
                Administrations = admins,
                Prescriptions = scripts
            };

            return dto;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PatientDto patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            var dbPat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            dbPat.firstName = patient.GivenName;
            dbPat.familyName = patient.FamilyName;
            dbPat.DOB = patient.DOB;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<PatientDto>> PostPatient(PatientDto patient)
        {
            var dbPatient = new Patient
            {
                firstName = patient.GivenName,
                familyName = patient.FamilyName,
                DOB = patient.DOB
            };

            _context.Patients.Add(dbPatient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = dbPatient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientDto>> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            var pat = new PatientDto
            {
                Id = id,
                FamilyName = patient.familyName,
                GivenName = patient.firstName,
                DOB = patient.DOB
            };

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return pat;
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
