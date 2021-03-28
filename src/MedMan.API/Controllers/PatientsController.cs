using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedMan.Application.Patients.Commands.CreatePatient;
using MedMan.Application.Patients.Commands.DeletePatient;
using MedMan.Application.Patients.Commands.UpdatePatient;
using MedMan.Application.Patients.Queries.Common;
using MedMan.Application.Patients.Queries.GetPatient;
using MedMan.Application.Patients.Queries.GetPatients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedMan.API.Controllers
{
    public class PatientsController : ApiControllerBase
    {
        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            return await Mediator.Send(new GetPatientsQuery());
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(int id)
        {
            return await Mediator.Send(new GetPatientQuery { Id = id });
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, UpdatePatientCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<int>> PostPatient(CreatePatientCommand command)
        {
            return await Mediator.Send(command);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await Mediator.Send(new DeletePatientCommand { Id = id });

            return NoContent();
        }
    }
}
