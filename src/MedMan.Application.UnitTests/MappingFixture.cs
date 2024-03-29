﻿using AutoMapper;

namespace MedMan.Application.UnitTests
{
    public class MappingFixture
    {
        public MappingFixture()
        {
            Mapper = MapperFactory.Create();
        }

        public IMapper Mapper { get; set; }
    }
}