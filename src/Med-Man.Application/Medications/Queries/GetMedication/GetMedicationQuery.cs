using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MedMan.Application.Common.Exceptions;
using MedMan.Application.Interfaces;
using MedMan.Application.Medications.Queries.Common;
using MedMan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedMan.Application.Medications.Queries.GetMedication
{
    public class GetMedicationQuery : IRequest<MedicationDto>
    {
        public int Id { get; set; }
    }

    public class GetMedicationQueryHandler : IRequestHandler<GetMedicationQuery, MedicationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMedicationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MedicationDto> Handle(GetMedicationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Medications
                .Where(m => m.Id == request.Id)
                .ProjectTo<MedicationDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity is null)
            {
                throw new NotFoundException(nameof(Medication), request.Id);
            }

            return entity;
        }
    }
}
