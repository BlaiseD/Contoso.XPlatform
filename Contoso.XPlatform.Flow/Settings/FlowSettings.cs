using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings.Navigation;
using Contoso.XPlatform.Flow.Settings.Screen;

namespace Contoso.XPlatform.Flow.Settings
{
    public class FlowSettings
    {
        public FlowSettings(ScreenSettingsBase screenSettings)
        {
            ScreenSettings = screenSettings;
        }

        public FlowSettings(FlowDataCache flowDataCache, NavigationBar navigationBar, ScreenSettingsBase screenSettings)
        {
            FlowDataCache = flowDataCache;
            NavigationBar = navigationBar;
            ScreenSettings = screenSettings;
        }

        public FlowDataCache FlowDataCache { get; set; }
        public NavigationBar NavigationBar { get; set; }
        public ScreenSettingsBase ScreenSettings { get; set; }
    }
}
