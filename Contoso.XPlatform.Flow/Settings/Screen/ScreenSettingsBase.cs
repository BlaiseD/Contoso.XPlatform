using Contoso.Forms.Configuration;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow.Settings.Screen
{
    public abstract class ScreenSettingsBase
    {
        abstract public ViewType ViewType { get; }
        public IList<CommandButtonDescriptor> CommandButtons { get; set; }
    }
}
