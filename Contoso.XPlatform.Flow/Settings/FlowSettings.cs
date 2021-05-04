using Contoso.Forms.Configuration.Navigation;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings.Screen;

namespace Contoso.XPlatform.Flow.Settings
{
    public class FlowSettings
    {
        public FlowSettings(ScreenSettingsBase screenSettings)
        {
            ScreenSettings = screenSettings;
        }

        public FlowSettings(FlowDataCache flowDataCache, NavigationBarDescriptor navigationBar, ScreenSettingsBase screenSettings)
        {
            FlowDataCache = flowDataCache;
            NavigationBar = navigationBar;
            ScreenSettings = screenSettings;
        }

        public FlowDataCache FlowDataCache { get; set; }
        public NavigationBarDescriptor NavigationBar { get; set; }
        public ScreenSettingsBase ScreenSettings { get; set; }
    }
}
