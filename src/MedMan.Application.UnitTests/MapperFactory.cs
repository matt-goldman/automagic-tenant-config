using AutoMapper;
using MedMan.Application.Common.Mappings;

namespace MedMan.Application.UnitTests
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return configurationProvider.CreateMapper();
        }
    }
}
