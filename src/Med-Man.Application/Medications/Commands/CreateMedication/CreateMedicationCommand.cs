using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Medications.Commands.CreateMedication
{
    public class CreateMedicationCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateMedicationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Medication
            {
                name = request.Name
            };

            _context.Medications.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
