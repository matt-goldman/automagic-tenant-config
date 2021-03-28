using MediatR;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Administrations.Commands.DeleteAdministration
{
    public class DeleteAdministrationCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteAdministrationCommandHandler : IRequestHandler<DeleteAdministrationCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAdministrationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAdministrationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Administrations.FindAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Administration), request.Id);
            }

            _context.Administrations.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
