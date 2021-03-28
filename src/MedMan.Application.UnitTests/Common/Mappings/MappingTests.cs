using AutoMapper;
using MedMan.Application.Administrations.Queries.Common;
using MedMan.Application.Medications.Queries.Common;
using MedMan.Application.Patients.Queries.Common;
using MedMan.Domain.Entities;
using System;
using Xunit;

namespace MedMan.Application.UnitTests.Common.Mappings
{
    public class MappingTests : IClassFixture<MappingFixture>
    {
        private readonly IMapper _mapper;

        public MappingTests(MappingFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _mapper
                .ConfigurationProvider
                .AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Patient), typeof(PatientDto))]
        [InlineData(typeof(Medication), typeof(MedicationDto))]
        [InlineData(typeof(Administration), typeof(AdministrationDto))]
        public void ShouldSupportMappingFromSourceToDestination
            (Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
