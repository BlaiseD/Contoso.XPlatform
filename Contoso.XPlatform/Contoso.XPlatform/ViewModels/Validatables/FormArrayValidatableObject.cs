using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormArrayValidatableObject<T, E> : ValidatableObjectBase<T> where T : ObservableCollection<E> where E : class
    {
        public FormArrayValidatableObject(string name, FormGroupArraySettingsDescriptor setting, IHttpService httpService, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
            : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            this.FormSettings = setting;
            this.formsCollectionDisplayTemplateDescriptor = setting.FormsCollectionDisplayTemplate;
            this.Title = setting.FormsCollectionDisplayTemplate.LoadingIndicatorText;
            this.httpService = httpService;
            this.Placeholder = this.formsCollectionDisplayTemplateDescriptor.LoadingIndicatorText;
            Entities = new ObservableCollection<E>();
            LoadEntities();
        }

        private readonly IHttpService httpService;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
        public IFormGroupSettings FormSettings { get; set; }
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate => formsCollectionDisplayTemplateDescriptor;

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

        private E _selectedItem;
        public E SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem == null || !_selectedItem.Equals(value))
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<E> _entities;
        public ObservableCollection<E> Entities
        {
            get
            {
                return _entities;
            }
            set
            {
                _entities = value ?? new ObservableCollection<E>();
                Value = (T)_entities;
                OnPropertyChanged();
            }
        }

        private async void LoadEntities()
        {
            try
            {
                GetListResponse response = await this.httpService.GetList
                (
                    new GetTypedListRequest
                    {
                        DataType = this.formsCollectionDisplayTemplateDescriptor.RequestDetails.DataType,
                        ModelType = this.formsCollectionDisplayTemplateDescriptor.RequestDetails.ModelType,
                        ModelReturnType = this.formsCollectionDisplayTemplateDescriptor.RequestDetails.ModelReturnType,
                        DataReturnType = this.formsCollectionDisplayTemplateDescriptor.RequestDetails.DataReturnType,
                        Selector = this.formsCollectionDisplayTemplateDescriptor.CollectionSelector
                    },
                    this.formsCollectionDisplayTemplateDescriptor.RequestDetails.DataSourceUrl
                );

                Entities = new ObservableCollection<E>(response.List.OfType<E>());
                this.Title = this.FormSettings.Title;
                this.Placeholder = this.formsCollectionDisplayTemplateDescriptor.PlaceHolderText;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
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
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PopModalAsync()
                        );
                    },
                    () => IsValid
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
                                new Views.ChildFormArrayPageCS(this)
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

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand != null)
                    return _editCommand;

                _editCommand = new Command
                (
                    () =>
                    {
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PushModalAsync
                            (
                                new Views.ChildFormPageCS
                                (
                                    new FormValidatableObject<E>
                                    (
                                        Entities.IndexOf(this.SelectedItem).ToString(),
                                        this.FormSettings,
                                        new IValidationRule[] { },
                                        this.uiNotificationService,
                                        this.httpService,
                                        App.ServiceProvider.GetRequiredService<IMapper>()
                                    )
                                    {
                                        Value = this.SelectedItem
                                    }
                                )
                            )
                        );
                    },
                    () => 
                    { 
                        return SelectedItem != null; 
                    }
                );

                return _editCommand;
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand != null)
                    return _addCommand;

                _addCommand = new Command
                (
                    () =>
                    {
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PushModalAsync
                            (
                                new Views.ChildFormPageCS
                                (
                                    new FormValidatableObject<E>
                                    (
                                        Entities.Count().ToString(),
                                        this.FormSettings,
                                        new IValidationRule[] { },
                                        this.uiNotificationService,
                                        this.httpService,
                                        App.ServiceProvider.GetRequiredService<IMapper>()
                                    )
                                    {
                                        Value = default
                                    }
                                )
                            )
                        );
                    }
                );

                return _addCommand;
            }
        }

        private ICommand _selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand != null)
                    return _selectionChangedCommand;

                _selectionChangedCommand = new Command
                (
                    () =>
                    {
                        (EditCommand as Command).ChangeCanExecute();
                    }
                );

                return _selectionChangedCommand;
            }
        }
    }
}
