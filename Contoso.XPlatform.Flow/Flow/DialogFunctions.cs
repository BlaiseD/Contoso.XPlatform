using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.TextForm;
using Contoso.Forms.Parameters.EditForm;
using Contoso.Forms.Parameters.ListForm;
using Contoso.Forms.Parameters.SearchForm;
using Contoso.Forms.Parameters.TextForm;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings.Screen;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow
{
    public class DialogFunctions : IDialogFunctions
    {
        public DialogFunctions(ScreenData screenData, FlowDataCache flowDataCache, IMapper mapper)
        {
            this.screenData = screenData;
            this.flowDataCache = flowDataCache;
            this.mapper = mapper;
        }

        #region Fields
        private readonly ScreenData screenData;
        private readonly FlowDataCache flowDataCache;
        private readonly IMapper mapper;
        #endregion Fields

        public void DisplayEditCollection([Comments("Configuration details for the form.")] SearchFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            throw new NotImplementedException();
        }

        public void DisplayEditForm([Comments("Configuration details for the form.")] EditFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            throw new NotImplementedException();
        }

        public void DisplayReadOnlyCollection([Comments("Configuration details for the form.")] ListFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            throw new NotImplementedException();
        }

        public void DisplayTextForm([Comments("Configuration details for the form.")] TextFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<TextFormSettingsDescriptor>
            (
                mapper.Map<TextFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.TextPage
            );
        }
    }
}
