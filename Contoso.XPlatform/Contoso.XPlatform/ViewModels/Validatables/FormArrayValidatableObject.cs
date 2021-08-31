﻿using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormArrayValidatableObject<T, E> : ValidatableObjectBase<T> where T : ObservableCollection<E> where E : class
    {
        public FormArrayValidatableObject(string name, FormGroupArraySettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService, IMapper mapper, IFieldsCollectionBuilder fieldsCollectionBuilder) 
            : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            this.FormSettings = setting;
            this.formsCollectionDisplayTemplateDescriptor = setting.FormsCollectionDisplayTemplate;
            this.Title = this.FormSettings.Title;
            this.Placeholder = setting.Placeholder;
            this.mapper = mapper;
            this.fieldsCollectionBuilder = fieldsCollectionBuilder;
        }

        private readonly IMapper mapper;
        private readonly IFieldsCollectionBuilder fieldsCollectionBuilder;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
        public IChildFormGroupSettings FormSettings { get; set; }
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
                    CheckCanExecute();
                }
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
                    Edit,
                    () => SelectedItem != null
                );

                return _editCommand;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                    return _deleteCommand;

                _deleteCommand = new Command
                (
                    () =>
                    {
                        Value.Remove(this.SelectedItem);
                        this.SelectedItem = null;
                    },
                    () => SelectedItem != null
                );

                return _deleteCommand;
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
                     Add
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
                    CheckCanExecute
                );

                return _selectionChangedCommand;
            }
        }

        private void CheckCanExecute()
        {
            (EditCommand as Command).ChangeCanExecute();
            (DeleteCommand as Command).ChangeCanExecute();
        }

        private void Edit()
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => App.Current.MainPage.Navigation.PushModalAsync
                (
                    new Views.ChildFormPageCS
                    (
                        new FormValidatableObject<E>
                        (
                            Value.IndexOf(this.SelectedItem).ToString(),
                            this.FormSettings,
                            new IValidationRule[] { },
                            this.uiNotificationService,
                            this.mapper,
                            this.fieldsCollectionBuilder
                        )
                        {
                            Value = this.SelectedItem
                        }
                    )
                )
            );
        }

        private void Add()
        {
            E newItem = Activator.CreateInstance<E>();
            Value.Add(newItem);
            SelectedItem = newItem;

            var addValidatable = new AddFormValidatableObject<E>
            (
                Value?.Count.ToString(),
                this.FormSettings,
                new IValidationRule[] { },
                this.uiNotificationService,
                this.mapper,
                this.fieldsCollectionBuilder
            )
            {
                Value = newItem
            };

            addValidatable.AddCancelled += AddValidatable_AddCancelled;

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => App.Current.MainPage.Navigation.PushModalAsync
                (
                    new Views.ChildFormPageCS(addValidatable)
                )
            );
        }

        private void AddValidatable_AddCancelled(object sender, System.EventArgs e)
        {
            Value.Remove(((AddFormValidatableObject<E>)sender).Value);
            SelectedItem = null; 
        }
    }
}
