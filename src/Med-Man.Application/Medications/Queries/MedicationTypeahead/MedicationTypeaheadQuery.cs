using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Application.Medications.Queries.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Medications.Queries.MedicationTypeahead
{
    public class MedicationTypeaheadQuery : IRequest<List<MedicationDto>>
    {
        public string SearchTerm { get; set; }
    }

    public class MedicationTypeaheadQueryHandler : IRequestHandler<MedicationTypeaheadQuery, List<MedicationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MedicationTypeaheadQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MedicationDto>> Handle(MedicationTypeaheadQuery request, CancellationToken cancellationToken)
        {
            return await _context.Medications
                .Where(m => m.name.ToLower().StartsWith(request.SearchTerm.ToLower()))
                .OrderBy(m => m.name)
                .Take(10)
                .ProjectTo<MedicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
