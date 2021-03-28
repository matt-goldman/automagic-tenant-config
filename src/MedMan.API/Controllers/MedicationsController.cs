using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedMan.Application.Medications.Commands.CreateMedication;
using MedMan.Application.Medications.Commands.DeleteMedication;
using Microsoft.AspNetCore.Authorization;
using MedMan.Application.Medications.Queries.Common;
using MedMan.Application.Medications.Queries.GetMedications;
using MedMan.Application.Medications.Queries.GetMedication;

namespace MedMan.API.Controllers
{
    public class MedicationsController : ApiControllerBase
    {
        // GET: api/Medications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> GetMedications()
        {
            return await Mediator.Send(new GetMedicationsQuery());
        }

        // GET: api/Medications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationDto>> GetMedication(int id)
        {
            return await Mediator.Send(new GetMedicationQuery { Id = id });
        }

        // POST: api/Medications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(Roles="Pharmacist")]
        public async Task<ActionResult<int>> PostMedication(string Name)
        {
            return await Mediator.Send(new CreateMedicationCommand { Name = Name });
        }

        // DELETE: api/Medications/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Pharmacist")]
        public async Task<ActionResult> DeleteMedication(int id)
        {
            await Mediator.Send(new DeleteMedicationCommand { Id = id });

            return NoContent();
        }
    }
}
