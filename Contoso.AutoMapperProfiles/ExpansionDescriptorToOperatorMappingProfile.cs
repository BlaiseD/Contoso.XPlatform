using AutoMapper;
using Contoso.Bsl.Configuration.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.Expansions;

namespace Contoso.AutoMapperProfiles
{
    public class ExpansionDescriptorToOperatorMappingProfile : Profile
    {
        public ExpansionDescriptorToOperatorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionDescriptor, SelectExpandDefinition>();
            CreateMap<SelectExpandItemFilterDescriptor, SelectExpandItemFilter>();
            CreateMap<SelectExpandItemDescriptor, SelectExpandItem>();
            CreateMap<SelectExpandItemQueryFunctionDescriptor, SelectExpandItemQueryFunction>();
        }
    }
}
