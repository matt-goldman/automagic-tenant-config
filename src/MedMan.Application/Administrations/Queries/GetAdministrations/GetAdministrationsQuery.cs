using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Administrations.Queries.Common;
using MedMan.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Administrations.Queries.GetAdministrations
{
    public class GetAdministrationsQuery : IRequest<List<AdministrationDto>>
    {
        
    }

    public class GetAdministrationsQueryHanlder : IRequestHandler<GetAdministrationsQuery, List<AdministrationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAdministrationsQueryHanlder(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AdministrationDto>> Handle(GetAdministrationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Administrations
                .ProjectTo<AdministrationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
