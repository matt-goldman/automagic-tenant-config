using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Application.Medications.Queries.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Medications.Queries.GetMedications
{
    public class GetMedicationsQuery : IRequest<List<MedicationDto>>
    {
        
    }

    public class GetMedicationsQueryHandler : IRequestHandler<GetMedicationsQuery, List<MedicationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMedicationsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MedicationDto>> Handle(GetMedicationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Medications
                .ProjectTo<MedicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
