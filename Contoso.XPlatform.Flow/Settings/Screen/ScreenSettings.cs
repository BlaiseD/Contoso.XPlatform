using Contoso.Forms.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.Flow.Settings.Screen
{
    public class ScreenSettings<TFormDescriptor> : ScreenSettingsBase
    {
        public ScreenSettings(TFormDescriptor settings, IEnumerable<CommandButtonDescriptor> commandButtons, ViewType viewType)
        {
            Settings = settings;
            CommandButtons = commandButtons;
            this.ViewType = viewType;
        }

        public override ViewType ViewType { get; }
        public TFormDescriptor Settings { get; set; }
    }
}
