using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Application.Patients.Queries.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Patients.Queries.PatientTypeahead
{
    public class PatientTypeaheadQuery : IRequest<List<PatientDto>>
    {
        public string SearchTerm { get; set; }
    }

    public class PatientTypeaheadQueryHandler : IRequestHandler<PatientTypeaheadQuery, List<PatientDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatientTypeaheadQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PatientDto>> Handle(PatientTypeaheadQuery request, CancellationToken cancellationToken)
        {
            return await _context.Patients
                .Where(p => p.firstName.ToLower().StartsWith(request.SearchTerm.ToLower()) 
                    || p.familyName.ToLower().StartsWith(request.SearchTerm.ToLower()))
                .OrderBy(p => p.familyName)
                .Take(10)
                .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
