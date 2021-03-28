using System.Collections.Generic;
using System.Threading.Tasks;
using MedMan.Application.Prescriptions.Commands;
using MedMan.Application.Prescriptions.Queries.Common;
using MedMan.Application.Prescriptions.Queries.GetPrescription;
using MedMan.Application.Prescriptions.Queries.GetPrescriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedMan.API.Controllers
{
    public class PrescriptionsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetPrescriptions()
        {
            return await Mediator.Send(new GetPrescriptionsQuery());
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDto>> GetPrescription(int id)
        {
            return await Mediator.Send(new GetPrescriptionQuery { Id = id });
        }

        // PUT: api/Prescriptions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //[Authorize(Roles = "Doctor")]
        //public async Task<IActionResult> PutPrescription(int id, UpdatePrescriptionCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        // POST: api/Prescriptions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<int>> PostPrescription(CreatePrescriptionCommand command)
        {
            return await Mediator.Send(command);
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            //await Mediator.Send(new DeletePrescriptionCommand { Id = id });

            return NoContent();
        }
    }
}
