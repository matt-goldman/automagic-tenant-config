using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Application.Patients.Queries.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Patients.Queries.GetPatients
{
    public class GetPatientsQuery : IRequest<List<PatientDto>>
    {
        
    }

    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, List<PatientDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPatientsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Patients
                .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
