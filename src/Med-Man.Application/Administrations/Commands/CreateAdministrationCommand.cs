using MediatR;
using MedMan.Application.Administrations.Common;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Administrations.Commands
{
    public class CreateAdministrationCommand : IRequest<int>
    {
        public AdministrationDto Administration { get; set; }
    }

    public class CreateAdministrationCommandHandler : IRequestHandler<CreateAdministrationCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateAdministrationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAdministrationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Administration
            {
                dose = request.Administration.Dose,
                medicationId = request.Administration.MedicationId,
                patientId = request.Administration.PatientId,
                timeGiven = request.Administration.TimeGiven
            };

            await _context.Administrations.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
