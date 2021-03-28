using System.Collections.Generic;
using System.Threading.Tasks;
using MedMan.Application.Administrations.Commands.CreateAdministration;
using MedMan.Application.Administrations.Commands.DeleteAdministration;
using MedMan.Application.Administrations.Commands.UpdateAdministration;
using MedMan.Application.Administrations.Queries.Common;
using MedMan.Application.Administrations.Queries.GetAdministration;
using MedMan.Application.Administrations.Queries.GetAdministrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedMan.API.Controllers
{
    [Authorize(Roles="Nurse,Doctor")]
    public class AdministrationsController : ApiControllerBase
    {
        // GET: api/Administrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministrationDto>>> GetAdministrations()
        {
            return await Mediator.Send(new GetAdministrationsQuery());
        }

        // GET: api/Administrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministrationDto>> GetAdministrations(int id)
        {
            return await Mediator.Send(new GetAdministrationQuery { Id = id });
        }

        // PUT: api/Administrations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrations(int id, UpdateAdministrationCommand command)
        {
            if (id != command.AdministrationId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        // POST: api/Administrations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostAdministrations(CreateAdministrationCommand command)
        {
            return await Mediator.Send(command);
        }

        // DELETE: api/Administrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdministrations(int id)
        {
            await Mediator.Send(new DeleteAdministrationCommand { Id = id });

            return NoContent();
        }
    }
}
