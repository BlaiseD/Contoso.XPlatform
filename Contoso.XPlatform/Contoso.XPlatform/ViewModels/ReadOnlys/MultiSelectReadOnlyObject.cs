﻿using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class MultiSelectReadOnlyObject<T, E> : ReadOnlyObjectBase<T> where T : ObservableCollection<E>
    {
        public MultiSelectReadOnlyObject(string name, MultiSelectDetailControlSettingsDescriptor setting, IContextProvider contextProvider) 
            : base(name, setting.MultiSelectTemplate.TemplateName)
        {
            this._multiSelectDetailControlSettingsDescriptor = setting;
            this._multiSelectTemplate = setting.MultiSelectTemplate;
            this.httpService = contextProvider.HttpService;
            this.Title = setting.Title;
            this.Placeholder = this._multiSelectTemplate.LoadingIndicatorText;
            itemComparer = new MultiSelectItemComparer<E>(_multiSelectDetailControlSettingsDescriptor.KeyFields);
            SelectedItems = new ObservableCollection<object>();
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly MultiSelectTemplateDescriptor _multiSelectTemplate;
        private readonly MultiSelectDetailControlSettingsDescriptor _multiSelectDetailControlSettingsDescriptor;
        private readonly MultiSelectItemComparer<E> itemComparer;

        public MultiSelectTemplateDescriptor MultiSelectTemplate => _multiSelectTemplate;

        public string DisplayText
        {
            get
            {
                if (Value == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(_multiSelectDetailControlSettingsDescriptor.StringFormat))
                    return GetText();

                return string.Format
                (
                    CultureInfo.CurrentCulture,
                    _multiSelectDetailControlSettingsDescriptor.StringFormat,
                    GetText()
                );

                string GetText()
                    => string.Join
                    (
                        ", ",
                        Value.Select
                        (
                            item => typeof(E).GetProperty(_multiSelectTemplate.TextField).GetValue(item)
                        )
                    );
            }
        }

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

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;

                UpdateSelectedItems();

                OnPropertyChanged(nameof(DisplayText));
                OnPropertyChanged(nameof(SelectedItems));
            }
        }

        ObservableCollection<object> _selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                if (_selectedItems != value)
                {
                    _selectedItems = value;
                }
            }
        }

        private List<E> _items;
        public List<E> Items
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
                BaseResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._multiSelectTemplate.RequestDetails.DataType,
                        ModelType = this._multiSelectTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._multiSelectTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._multiSelectTemplate.RequestDetails.DataReturnType,
                        Selector = this.MultiSelectTemplate.TextAndValueSelector
                    },
                    this._multiSelectTemplate.RequestDetails.DataSourceUrl
                );

                if (response?.Success != true)
                {
#if DEBUG
                    await App.Current.MainPage.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }

                Items = ((GetListResponse)response).List.OfType<E>().ToList();
                UpdateSelectedItems();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private void UpdateSelectedItems()
        {
            if (Items?.Any() != true)
                return;

            var selected = Value?.Any() != true
                ? Enumerable.Empty<object>()
                : Items.Where(i => Value.Contains(i, itemComparer)).Cast<object>();

            SelectedItems.Clear();
            foreach (var item in selected)
                SelectedItems.Add(item);

            this.Placeholder = this._multiSelectTemplate.PlaceholderText;
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
                                new Views.ReadOnlyMultiSelectPageCS(this)
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
