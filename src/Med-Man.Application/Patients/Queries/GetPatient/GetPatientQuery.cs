using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Application.Patients.Queries.Common;
using MedMan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Patients.Queries.GetPatient
{
    public class GetPatientQuery : IRequest<PatientDto>
    {
        public int Id { get; set; }
    }

    public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PatientDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPatientQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                .Where(p => p.Id == request.Id)
                .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if(patient is null)
            {
                throw new NotFoundException(nameof(Patient), request.Id);
            }

            return patient;
        }
    }
}
