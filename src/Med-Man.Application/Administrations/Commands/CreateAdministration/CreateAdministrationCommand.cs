using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Administrations.Commands.CreateAdministration
{
    public class CreateAdministrationCommand : IRequest<int>
    {
        public int MedicationId { get; set; }
        public int PatientId { get; set; }
        public int Dose { get; set; }
        public DateTime TimeGiven { get; set; }
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
                dose = request.Dose,
                medicationId = request.MedicationId,
                patientId = request.PatientId,
                timeGiven = request.TimeGiven
            };

            await _context.Administrations.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
