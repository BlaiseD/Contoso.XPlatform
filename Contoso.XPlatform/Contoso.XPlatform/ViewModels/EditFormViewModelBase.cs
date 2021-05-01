using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels
{
    public abstract class EditFormViewModelBase : ViewModelBase
    {
        public EditFormViewModelBase()
        {
        }

        protected EditFormViewModelBase(ScreenSettings<EditFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService, IHttpService httpService)
        {
            this.UiNotificationService = uiNotificationService;
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            fieldsCollectionHelper = new FieldsCollectionHelper(FormSettings, Properties, this.UiNotificationService, httpService);
            fieldsCollectionHelper.CreateFieldsCollection();
        }

        public EditFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<IValidatable> Properties { get; set; } = new ObservableCollection<IValidatable>();
        public UiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        internal readonly FieldsCollectionHelper fieldsCollectionHelper;
        protected IDictionary<string, object> values;

        public ICommand SubmitCommand => new Command
        (
            execute: async () =>
            {
                await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
            },
            canExecute: () => AreFieldsValid()
        );

        public ICommand NavigateCommand => new Command(async () =>
        {
            await App.Current.MainPage.DisplayAlert("Navigate", "Navigate", "Ok");
        });

        public bool AreFieldsValid()
            => Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );
    }
}
