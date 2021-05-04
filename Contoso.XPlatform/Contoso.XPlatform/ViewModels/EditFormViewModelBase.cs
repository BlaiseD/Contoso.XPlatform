using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels
{
    public abstract class EditFormViewModelBase : ViewModelBase, IDisposable
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
            propertyChangedSubscription = this.UiNotificationService.ValueChanged.Subscribe(FieldChanged);
        }

        public EditFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<IValidatable> Properties { get; set; } = new ObservableCollection<IValidatable>();
        public UiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        internal readonly FieldsCollectionHelper fieldsCollectionHelper;
        protected IDictionary<string, object> values;
        private readonly IDisposable propertyChangedSubscription;

        private ICommand _submitCommand;
        public ICommand SubmitCommand => _submitCommand ??= new Command<CommandButtonDescriptor>
        (
            execute: async (button) =>
            {
                //foreach (var property in Properties)
                //    property.IsDirty = true;

                AreFieldsValid();

                await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
            },
            canExecute: (button) => AreFieldsValid()
        );

        private ICommand _navigateCommand;
        public ICommand NavigateCommand => _navigateCommand ??= new Command
        (
            async () =>
            {
                await App.Current.MainPage.DisplayAlert("Navigate", "Navigate", "Ok");
            }
        );

        public bool AreFieldsValid()
            => Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );

        private void FieldChanged(string fieldName)
        {
            (SubmitCommand as Command).ChangeCanExecute();
        }

        public virtual void Dispose()
        {
            Dispose(this.propertyChangedSubscription);
        }

        protected void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
