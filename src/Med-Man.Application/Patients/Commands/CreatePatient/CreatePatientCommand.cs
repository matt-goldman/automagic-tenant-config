using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Patients.Commands.CreatePatient
{
    public class CreatePatientCommand : IRequest<int>
    {
        public DateTime DOB { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
    }

    public class CreatePatientCommandHanlder : IRequestHandler<CreatePatientCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePatientCommandHanlder(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var entity = new Patient
            {
                DOB = request.DOB,
                familyName = request.FamilyName,
                firstName = request.FirstName
            };

            _context.Patients.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
