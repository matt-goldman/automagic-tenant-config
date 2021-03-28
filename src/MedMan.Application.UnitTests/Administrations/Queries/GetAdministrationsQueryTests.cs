using AutoMapper;
using FluentAssertions;
using MedMan.Application.Administrations.Queries.Common;
using MedMan.Application.Administrations.Queries.GetAdministrations;
using MedMan.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MedMan.Application.UnitTests.Administrations.Queries
{
    [Collection(nameof(QueryCollection))]
    public class GetAdministrationsQueryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAdministrationsQueryTests()
        {
            _context = DbContextFactory.Create();
            _mapper = MapperFactory.Create();
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndListCount()
        {
            // Arrange
            var query = new GetAdministrationsQuery();
            var handler = new GetAdministrationsQueryHanlder(_context, _mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeOfType<List<AdministrationDto>>();
            result.Should().HaveCount(1);
            result[0].PatientName.Should().Be("John Smith");
        }
    }
}
