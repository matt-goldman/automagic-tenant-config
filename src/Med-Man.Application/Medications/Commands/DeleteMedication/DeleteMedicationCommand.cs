using MediatR;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Medications.Commands.DeleteMedication
{
    public class DeleteMedicationCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteMedicationCommandHandler : IRequestHandler<DeleteMedicationCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteMedicationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Medications.FindAsync(new int[] { request.Id }, cancellationToken);

            if(entity is null)
            {
                throw new NotFoundException(nameof(Medication), request.Id);
            }

            _context.Medications.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
