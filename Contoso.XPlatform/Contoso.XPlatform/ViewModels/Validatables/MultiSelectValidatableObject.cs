using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class MultiSelectValidatableObject<T> : ValidatableObjectBase<T> where T : ObservableCollection<object>
    {
        public MultiSelectValidatableObject(string name, MultiSelectFormControlSettingsDescriptor setting, IHttpService httpService, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
            : base(name, setting.MultiSelectTemplate.TemplateName, validations, uiNotificationService)
        {
            this.Title = setting.Title;
            this._multiSelectTemplate = setting.MultiSelectTemplate;
            this.httpService = httpService;
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly MultiSelectTemplateDescriptor _multiSelectTemplate;

        public MultiSelectTemplateDescriptor MultiSelectTemplate => _multiSelectTemplate;

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

        private ObservableCollection<object> _selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }

            set
            {
                _selectedItems = value;
                Value = (T)_selectedItems;
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
                    new GetTypedDropDownListRequest
                    {
                        DataType = this._multiSelectTemplate.RequestDetails.DataType,
                        ModelType = this._multiSelectTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._multiSelectTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._multiSelectTemplate.RequestDetails.DataReturnType,
                        Selector = this.MultiSelectTemplate.TextAndValueSelector
                    },
                    this._multiSelectTemplate.RequestDetails.DataSourceUrl
                );

                Items = response.DropDownList.OfType<object>().ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }
    }
}
