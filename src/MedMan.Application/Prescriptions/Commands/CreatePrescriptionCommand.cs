using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Prescriptions.Commands
{
    public class CreatePrescriptionCommand : IRequest<int>
    {
        public int PatientId { get; set; }
        public int MedicationId { get; set; }
        public int Dose { get; set; }
    }

    public class CreatePRescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePRescriptionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Prescription
            {
                medicationId = request.MedicationId,
                patientId = request.PatientId,
                dose = request.Dose
            };

            _context.Prescriptions.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
