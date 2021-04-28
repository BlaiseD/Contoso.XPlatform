using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class PickerValidatableObject<T> : ValidatableObjectBase<T>
    {
        public PickerValidatableObject(FormControlSettingsDescriptor setting, IHttpService httpService, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
            : base(setting.Field, setting.DropDownTemplate.TemplateName, validations, uiNotificationService)
        {
            this.Title = setting.Title;
            this.dropDownTemplate = setting.DropDownTemplate;
            this.httpService = httpService;
            GetItemSource();
        }

        private void GetItemSource()
        {
            throw new NotImplementedException();
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor dropDownTemplate;

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

        private List<T> _items;
        public List<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectedIndexChangedCommand => new Command(() => IsValid = Validate());
    }
}
