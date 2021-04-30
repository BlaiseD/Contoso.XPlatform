using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels
{
    public class EditFormViewModel<TModel> : ViewModelBase, IDisposable
    {
        public EditFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<IValidatable> Properties { get; set; } = new ObservableCollection<IValidatable>();
        public UiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private IDictionary<string, object> values;
        private readonly FieldsCollectionHelper fieldsCollectionHelper;
        private CommandButtonDescriptor _selectedButton;
        private readonly ValidateIfManager<TModel> validateIfManager;

        public EditFormViewModel()
        {

        }
        public EditFormViewModel(EditFormSettingsDescriptor editFormSettingsDescriptor, UiNotificationService uiNotificationService, IMapper mapper, IHttpService httpService)
        {
            this.UiNotificationService = uiNotificationService;
            FormSettings = editFormSettingsDescriptor;

            fieldsCollectionHelper = new FieldsCollectionHelper(FormSettings, Properties, this.UiNotificationService, httpService);
            fieldsCollectionHelper.CreateFieldsCollection();
            this.validateIfManager = new ValidateIfManager<TModel>
            (
                Properties,
                fieldsCollectionHelper.GetConditionalValidationConditions<TModel>
                (
                    FormSettings.ConditionalDirectives,
                    Properties,
                    mapper
                ),
                mapper,
                this.UiNotificationService
            );

        }

        public EditFormViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService, IMapper mapper, IHttpService httpService)
        {
            this.UiNotificationService = uiNotificationService;
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);

            fieldsCollectionHelper = new FieldsCollectionHelper(FormSettings, Properties, this.UiNotificationService, httpService);
            fieldsCollectionHelper.CreateFieldsCollection();
            this.validateIfManager = new ValidateIfManager<TModel>
            (
                Properties,
                fieldsCollectionHelper.GetConditionalValidationConditions<TModel>
                (
                    FormSettings.ConditionalDirectives,
                    Properties,
                    mapper
                ),
                mapper,
                this.UiNotificationService
            );

        }

        public CommandButtonDescriptor SelectedButton
        {
            get { return _selectedButton; }
            set
            {
                if (_selectedButton == value)
                    return;

                _selectedButton = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand => new Command(async () =>
        {
            if (!AreFieldsValid())
                return;

            await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
        });

        public ICommand NavigateCommand => new Command(async () =>
        {
            await App.Current.MainPage.DisplayAlert("Navigate", "Navigate", "Ok");
        });

        bool AreFieldsValid()
            => Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );

        public void Dispose()
        {
            Dispose(this.validateIfManager);
        }

        private void Dispose(IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
