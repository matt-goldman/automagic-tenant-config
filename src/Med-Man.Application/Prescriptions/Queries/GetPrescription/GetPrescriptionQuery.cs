using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Application.Prescriptions.Queries.Common;
using MedMan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Prescriptions.Queries.GetPrescription
{
    public class GetPrescriptionQuery : IRequest<PrescriptionDto>
    {
        public int Id { get; set; }
    }

    public class GetPrescriptionQueryHandler : IRequestHandler<GetPrescriptionQuery, PrescriptionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPrescriptionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrescriptionDto> Handle(GetPrescriptionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Prescriptions
                .Where(p => p.Id == request.Id)
                .ProjectTo<PrescriptionDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Prescription), request.Id);
            }

            return entity;
        }
    }
}
