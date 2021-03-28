using MediatR;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Administrations.Commands.UpdateAdministration
{
    public class UpdateAdministrationCommand : IRequest
    {
        public int MedicationId { get; set; }
        public int PatientId { get; set; }
        public int Dose { get; set; }
        public DateTime TimeGiven { get; set; }

        public int AdministrationId { get; set; }
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
            var entity = await _context.Administrations.FindAsync(request.AdministrationId);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Administration), request.AdministrationId);
            }

            entity.medicationId = request.MedicationId;
            entity.patientId = request.PatientId;
            entity.dose = request.Dose;
            entity.timeGiven = request.TimeGiven;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
