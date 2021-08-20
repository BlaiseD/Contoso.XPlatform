using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.SearchPage
{
    public class SearchPageListViewModelBase : ViewModelBase, IDisposable
    {
        protected SearchPageListViewModelBase(ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
        }

        public SearchFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
