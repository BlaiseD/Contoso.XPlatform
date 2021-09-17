using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class PickerReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public PickerReadOnlyObject(string name, DetailControlSettingsDescriptor setting, IContextProvider contextProvider) : base(name, setting.DropDownTemplate.TemplateName)
        {
            this._dropDownTemplate = setting.DropDownTemplate;
            this.httpService = contextProvider.HttpService;
            DetailControlSettingsDescriptor = setting;
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private List<object> _items;

        public DetailControlSettingsDescriptor DetailControlSettingsDescriptor { get; }
        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

        public string DisplayText
        {
            get
            {
                if (SelectedItem == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(DetailControlSettingsDescriptor.StringFormat))
                    return SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField);

                return string.Format
                (
                    CultureInfo.CurrentCulture, 
                    DetailControlSettingsDescriptor.StringFormat,
                    SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField)
                );
            }
        }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public object SelectedItem
        {
            get
            {
                if (_items?.Any() != true)
                    return null;

                return _items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );
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

                _items = null;
                _items = response.DropDownList.Cast<object>().ToList();
                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(DisplayText));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }
    }
}
