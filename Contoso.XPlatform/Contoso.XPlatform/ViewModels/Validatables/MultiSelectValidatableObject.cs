﻿using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class MultiSelectValidatableObject<T, E> : ValidatableObjectBase<T> where T : ObservableCollection<E>
    {
        public MultiSelectValidatableObject(string name, MultiSelectFormControlSettingsDescriptor setting, IHttpService httpService, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService)
            : base(name, setting.MultiSelectTemplate.TemplateName, validations, uiNotificationService)
        {
            this.Title = setting.Title;
            this._multiSelectFormControlSettingsDescriptor = setting;
            this._multiSelectTemplate = setting.MultiSelectTemplate;
            this.httpService = httpService;
            itemComparer = new MultiSelectItemComparer<E>(_multiSelectFormControlSettingsDescriptor.KeyFields);
            SelectedItems = new ObservableCollection<object>();
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly MultiSelectTemplateDescriptor _multiSelectTemplate;
        private readonly MultiSelectFormControlSettingsDescriptor _multiSelectFormControlSettingsDescriptor;
        private readonly MultiSelectItemComparer<E> itemComparer;

        public MultiSelectTemplateDescriptor MultiSelectTemplate => _multiSelectTemplate;

        public string DisplayText
        {
            get
            {
                if (Value == null)
                    return string.Empty;

                return string.Join
                (
                    ",",
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

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;

                //UpdateSelectedItems();

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

                Items = response.DropDownList.OfType<E>().ToList();
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
                        Value = (T)new ObservableCollection<E>
                        (
                            Items.Where(i => SelectedItems.Cast<E>().Contains(i, itemComparer))
                        );

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
                                new Views.MultiSelectPageCS(this)
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