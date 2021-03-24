using MediatR;
using MedMan.Application.Administrations.Common;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Administrations.Commands
{
    public class UpdateAdministrationCommand : IRequest
    {
        public AdministrationDto Administration { get; set; }
    }

    public class UpdateAdministrationCommandHandler : IRequestHandler<UpdateAdministrationCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAdministrationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAdministrationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Administrations.FindAsync(request.Administration.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Administration), request.Administration.Id);
            }

            entity.medicationId = request.Administration.MedicationId;
            entity.patientId = request.Administration.PatientId;
            entity.dose = request.Administration.Dose;
            entity.timeGiven = request.Administration.TimeGiven;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
