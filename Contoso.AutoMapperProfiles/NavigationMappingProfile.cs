using AutoMapper;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Parameters.Navigation;

namespace Contoso.AutoMapperProfiles
{
    public class NavigationMappingProfile : Profile
    {
        public NavigationMappingProfile()
        {
            CreateMap<NavigationBarParameters, NavigationBarDescriptor>();
            CreateMap<NavigationMenuItemParameters, NavigationMenuItemDescriptor>();
        }
    }
}
