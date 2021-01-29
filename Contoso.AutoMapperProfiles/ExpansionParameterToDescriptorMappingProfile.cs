﻿using AutoMapper;
using Contoso.Bsl.Configuration.ExpansionDescriptors;
using Contoso.Parameters.Expansions;

namespace Contoso.AutoMapperProfiles
{
    public class ExpansionParameterToDescriptorMappingProfile : Profile
    {
        public ExpansionParameterToDescriptorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionParameters, SelectExpandDefinitionDescriptor>();
            CreateMap<SelectExpandItemFilterParameters, SelectExpandItemFilterDescriptor>();
            CreateMap<SelectExpandItemParameters, SelectExpandItemDescriptor>();
            CreateMap<SelectExpandItemQueryFunctionParameters, SelectExpandItemQueryFunctionDescriptor>();
        }
    }
}
