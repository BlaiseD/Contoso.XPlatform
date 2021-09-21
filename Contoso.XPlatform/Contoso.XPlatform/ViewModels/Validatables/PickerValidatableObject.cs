using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
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
        public PickerValidatableObject(string name, FormControlSettingsDescriptor setting, IEnumerable<IValidationRule> validations, IContextProvider contextProvider) 
            : base(name, setting.DropDownTemplate.TemplateName, validations, contextProvider.UiNotificationService)
        {       
            this.controlSettings = setting;
            this._dropDownTemplate = setting.DropDownTemplate;
            this.httpService = contextProvider.HttpService;
            this.Title = this._dropDownTemplate.LoadingIndicatorText;
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private readonly FormControlSettingsDescriptor controlSettings;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

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

                return Items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
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
                Value = _selectedItem == null 
                    ? default 
                    : _selectedItem.GetPropertyValue<T>(_dropDownTemplate.ValueField);

                OnPropertyChanged();
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

        private async void GetItemSource()
        {
            try
            {
                GetObjectDropDownListResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._dropDownTemplate.RequestDetails.DataType,
                        ModelType = this._dropDownTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._dropDownTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._dropDownTemplate.RequestDetails.DataReturnType,
                        Selector = this.DropDownTemplate.TextAndValueSelector
                    },
                    this._dropDownTemplate.RequestDetails.DataSourceUrl
                );

                if (response?.Success != true)
                {
                    await App.Current.MainPage.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
                    return;
                }

                Items = null;
                await System.Threading.Tasks.Task.Delay(400);
                Items = response.DropDownList.Cast<object>().ToList();
                OnPropertyChanged(nameof(SelectedItem));

                this.Title = controlSettings.Title;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
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
