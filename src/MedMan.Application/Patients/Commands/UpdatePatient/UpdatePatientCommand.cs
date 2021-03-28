using MediatR;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommand : IRequest
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
    }

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePatientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Patients.FindAsync(new int[] { request.Id }, cancellationToken);

            if(entity is null)
            {
                throw new NotFoundException(nameof(Patient), request.Id);
            }

            entity.familyName = request.FamilyName;
            entity.firstName = request.FirstName;
            entity.DOB = request.DOB;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
