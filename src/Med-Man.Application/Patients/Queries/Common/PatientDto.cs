using AutoMapper;
using MedMan.Application.Common.Mappings;
using MedMan.Domain.Entities;
using System;

namespace MedMan.Application.Patients.Queries.Common
{
    public class PatientDto : IMapFrom<Patient>
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string FullName => string.Format("{0} {1}", GivenName, FamilyName);
        public DateTime DOB { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Patient, PatientDto>()
                .ForMember(p => p.GivenName, opt => opt.MapFrom(src => src.familyName));
        }
    }
}
