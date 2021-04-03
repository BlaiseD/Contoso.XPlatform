using AutoMapper;
using Contoso.Common.Configuration.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.AutoMapperProfiles
{
    public class ExpansionDescriptorToOperatorMappingProfile : Profile
    {
        public ExpansionDescriptorToOperatorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionDescriptor, SelectExpandDefinition>();
            CreateMap<SelectExpandItemFilterDescriptor, SelectExpandItemFilter>()
                .ForMember(dest => dest.FilterBody, opts => opts.Ignore())
                .ForMember(dest => dest.ParameterName, opts => opts.Ignore());
            CreateMap<SelectExpandItemDescriptor, SelectExpandItem>();
            CreateMap<SelectExpandItemQueryFunctionDescriptor, SelectExpandItemQueryFunction>()
                .ForMember(dest => dest.MethodCallDescriptor, opts => opts.Ignore());
            CreateMap<SortCollectionDescriptor, SortCollection>()
                .ForMember(dest => dest.Skip, opts => opts.MapFrom(src => src.Skip.HasValue ? src.Skip.Value : 0))
                .ForMember(dest => dest.Take, opts => opts.MapFrom(src => src.Take.HasValue ? src.Take.Value : int.MaxValue));
            CreateMap<SortDescriptionDescriptor, SortDescription>();
        }
    }
}
