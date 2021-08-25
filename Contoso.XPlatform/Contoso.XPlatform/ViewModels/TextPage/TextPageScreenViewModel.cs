using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.TextPage
{
    public class TextPageScreenViewModel : ViewModelBase, IDisposable
    {
        public TextPageScreenViewModel(ScreenSettings<TextFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            Title = this.FormSettings.Title;
        }

        public TextFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
