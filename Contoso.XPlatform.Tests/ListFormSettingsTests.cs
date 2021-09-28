using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Parameters.Bindings;
using Contoso.Forms.Parameters.ListForm;
using System.Collections.Generic;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class ListFormSettingsTests
    {
        public ListFormSettingsTests()
        {
            SetupAutoMapper();
        }

        #region Fields
        static IMapper mapper;
        #endregion Fields

        [Fact]
        public void Map_ConnectorParameters_To_CommandButtonDescriptor()
        {
            ListFormSettingsParameters parameters = new ListFormSettingsParameters
            (
                "About",
                typeof(LookUpsModel),
                "Loading ...",
                "TextDetailTemplate",
                new List<ItemBindingParameters>
                {
                    new ItemBindingParameters
                    (
                        "Text",
                        "DateTimeValue",
                        "Enrollment Date: {0:MM/dd/yyyy}"
                    )
                },
                null,
                null
            );
            ListFormSettingsDescriptor settings = mapper.Map<ListFormSettingsDescriptor>(parameters);
            Assert.Equal("TextDetailTemplate", settings.ItemTemplateName);
        }

        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FormsParameterToFormsDescriptorMappingProfile>();
                cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                cfg.AddProfile<SchoolProfile>();
                cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                cfg.AddProfile<ItemFilterParameterToDescriptorMappingProfile>();
            });
            config.AssertConfigurationIsValid<FormsParameterToFormsDescriptorMappingProfile>();
            mapper = config.CreateMapper();
        }
    }
}
