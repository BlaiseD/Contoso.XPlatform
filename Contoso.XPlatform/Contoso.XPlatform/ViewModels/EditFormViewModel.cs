using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
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

        private IDictionary<string, object> values;
        private readonly FieldsCollectionHelper fieldsCollectionHelper;
        private CommandButtonDescriptor _selectedButton;
        private readonly ValidateIfManager<TModel> validateIfManager;

        public EditFormViewModel(EditFormSettingsDescriptor formSettings, UiNotificationService uiNotificationService, IMapper mapper, IHttpService httpService)
        {
            this.UiNotificationService = uiNotificationService;
            FormSettings = formSettings;
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

        public ICommand SignUpCommand => new Command(async () =>
        {
            if (!AreFieldsValid())
                return;

            await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
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
