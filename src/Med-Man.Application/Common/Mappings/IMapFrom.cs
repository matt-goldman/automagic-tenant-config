using AutoMapper;

namespace MedMan.Application.Common.Mappings
{
    public interface IMapFrom <T>
    {
        void Mapping(Profile Profile)
            => Profile.CreateMap(typeof(T), GetType());
    }
}
