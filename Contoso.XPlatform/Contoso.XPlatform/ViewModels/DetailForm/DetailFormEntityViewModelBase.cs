﻿using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.DetailForm
{
    public abstract class DetailFormEntityViewModelBase : ViewModelBase
    {
        protected DetailFormEntityViewModelBase(ScreenSettings<DetailFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
        {
            this.UiNotificationService = contextProvider.UiNotificationService;
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            Properties = contextProvider.ReadOnlyFieldsCollectionBuilder.CreateFieldsCollection(this.FormSettings);
        }

        public Dictionary<string, IReadOnly> PropertiesDictionary
            => Properties.ToDictionary(p => p.Name);

        public DetailFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<IReadOnly> Properties { get; set; }
        public UiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand != null)
                    return _nextCommand;

                _nextCommand = new Command<CommandButtonDescriptor>
                (
                     Next
                );

                return _nextCommand;
            }
        }

        protected void Next(CommandButtonDescriptor button)
        {
            NavigateNext(button);
        }

        private Task NavigateNext(CommandButtonDescriptor button)
            => this.UiNotificationService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );

    }
}