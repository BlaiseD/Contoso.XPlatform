using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async void GetItemSource()
        {
            try
            {
                GetAnonymousDropDownListResponse response = await this.httpService.GetAnonymousDropDown
                (
                    new GetAnonymousDropDownListRequest
                    {
                        DataType = this.dropDownTemplate.RequestDetails.DataType,
                        ModelType = this.dropDownTemplate.RequestDetails.ModelType,
                        Selector = this.DropDownTemplate.TextAndValueSelector
                    },
                    this.dropDownTemplate.RequestDetails.DataSourceUrl
                );

                Items = response.DropDownList.ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor dropDownTemplate;

        public DropDownTemplateDescriptor DropDownTemplate => dropDownTemplate;

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

        private object _selectedItem;
        public object SelectedItem
        {
            get
            {
                if (Items?.Any() != true)
                    return null;

                Type itemType = Items.First().GetType();
                return Items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        (T)itemType.GetProperty(dropDownTemplate.ValueField).GetValue(i)
                    )
                );
            }

            set
            {
                if (_selectedItem == null && value == null)
                    return;

                if (_selectedItem != null && _selectedItem.Equals(value))
                    return;

                _selectedItem = value;
                Value = _selectedItem == null ?
                    default :
                    (T)_selectedItem.GetType().GetProperty(dropDownTemplate.ValueField).GetValue(_selectedItem);
            }
        }

        private List<object> _items;
        public List<object> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectedIndexChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
