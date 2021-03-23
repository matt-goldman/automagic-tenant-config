using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Administrations.Common;
using MedMan.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Administrations.Queries
{
    public class GetAdministrationQuery : IRequest<AdministrationDto>
    {
        public int Id { get; set; }
    }

    public class GetAdministationQueryHandler : IRequestHandler<GetAdministrationQuery, AdministrationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAdministationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AdministrationDto> Handle(GetAdministrationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Administrations
                .Where(a => a.Id == request.Id)
                .ProjectTo<AdministrationDto>(_mapper.ConfigurationProvider)
                .SingleAsync();
        }
    }
}
