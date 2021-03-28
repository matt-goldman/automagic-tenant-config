using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Interfaces;
using MedMan.Application.Prescriptions.Queries.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Prescriptions.Queries.GetPrescriptions
{
    public class GetPrescriptionsQuery : IRequest<List<PrescriptionDto>>
    {
        
    }

    public class GetPrescriptionsQueryHandler : IRequestHandler<GetPrescriptionsQuery, List<PrescriptionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPrescriptionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PrescriptionDto>> Handle(GetPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Prescriptions
                .ProjectTo<PrescriptionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
