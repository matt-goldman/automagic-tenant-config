using MediatR;
using MedMan.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Patients.Commands.DeletePatient
{
    public class DeletePatientCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePatientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Patients.FindAsync(new int[] { request.Id }, cancellationToken);

            _context.Patients.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
