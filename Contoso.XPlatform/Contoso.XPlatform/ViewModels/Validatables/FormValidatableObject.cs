using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormValidatableObject<T> : ValidatableObjectBase<T> where T : class
    {
        public FormValidatableObject(string name, IFormGroupSettings setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService, IHttpService httpService, IMapper mapper) : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            this.FormSettings = setting;
            this.Title = this.FormSettings.Title;
            this.Placeholder = this.FormSettings.ValidFormControlText;
            this.mapper = mapper;
            FieldsCollectionHelper fieldsCollectionHelper = new FieldsCollectionHelper(setting, Properties, uiNotificationService, httpService, this.mapper);
            fieldsCollectionHelper.CreateFieldsCollection();
        }

        public ObservableCollection<IValidatable> Properties { get; set; } = new ObservableCollection<IValidatable>();
        
        public IFormGroupSettings FormSettings { get; set; }
        private readonly IMapper mapper;

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

        public string DisplayText => string.Empty;

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; 
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                Properties.UpdateValidatables(base.Value, FormSettings.FieldSettings, this.mapper);

                IsValid = Validate();

                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public ICommand TextChangedCommand => new Command
        (
            (parameter) =>
            {
                IsDirty = true;
                string text = ((TextChangedEventArgs)parameter).NewTextValue;
                if (text == null)
                    return;

                IsValid = Validate();
            }
        );

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand != null)
                    return _submitCommand;

                _submitCommand = new Command
                (
                    () =>
                    {
                        Value = (T)Properties.ToModelObject
                        (
                            typeof(T), 
                            this.mapper, 
                            this.FormSettings.FieldSettings,
                            Value
                        );

                        Placeholder = IsValid ? this.FormSettings.ValidFormControlText : this.FormSettings.InvalidFormControlText;

                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PopModalAsync()
                        );
                    }
                );

                return _submitCommand;
            }
        }

        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand != null)
                    return _openCommand;

                _openCommand = new Command
                (
                    () =>
                    {
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PushModalAsync
                            (
                                new Views.ChildFormPageCS(this)
                                {
                                    //BindingContext = this
                                }
                            )
                        );
                    });

                return _openCommand;
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand != null)
                    return _cancelCommand;

                _cancelCommand = new Command
                (
                    () =>
                    {
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PopModalAsync()
                        );
                    });

                return _cancelCommand;
            }
        }
    }
}
