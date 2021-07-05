using Contoso.Forms.Configuration;
using Contoso.XPlatform.Behaviours;
using Contoso.XPlatform.Converters;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    internal static class EditFormViewHelpers
    {
        public static CommandButtonSelector GetCommandButtonSelector(Type viewModelType, EventHandler buttonTappedHandler) => new CommandButtonSelector
        {
            SubmitButtonTemplate = GetButtonTemplate("SubmitCommand", viewModelType, buttonTappedHandler),
            NavigateButtonTemplate = GetButtonTemplate("NavigateCommand", viewModelType, buttonTappedHandler)
        };

        public static MultiSelectItemTemplateSelector GetMultiSelectItemTemplateSelector(MultiSelectTemplateDescriptor multiSelectTemplateDescriptor)
        {
            return new MultiSelectItemTemplateSelector
            {
                SingleFieldTemplate = new DataTemplate
                (
                    () => new Grid
                    {
                        Padding = new Thickness(10),
                        Style = LayoutHelpers.GetStaticStyleResource("MultiSelectItemStyle"),
                        Children =
                        {
                            new Label 
                            { 
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center,
                                FontAttributes = FontAttributes.Bold
                            }.AddBinding(Label.TextProperty, new Binding(multiSelectTemplateDescriptor.TextField))
                        }
                    }
                )
            };
        }

        public static QuestionTemplateSelector QuestionTemplateSelector { get; } = new QuestionTemplateSelector
        {
            TextTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetEntryForValidation(),
                        GetLabelForValidation()
                    }
                }
            ),
            PasswordTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetPasswordEntryForValidation(),
                        GetLabelForValidation()
                    }
                }
            ),
            DateTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                       GetDatePickerForValidation(),
                       GetLabelForValidation()
                    }
                }
            ),
            CheckboxTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetCheckboxForValidation(),
                        GetLabelForValidation()
                    }
                }
            ),
            PickerTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetPickerForValidation(),
                        GetLabelForValidation()
                    }
                }
            ),
            MultiSelectTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetMultiSelectFieldControl(),
                        GetLabelForValidation()
                    }
                }
            )
        };

        public static StackLayout GetCheckboxForValidation()
            => new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new CheckBox
                    {
                        Behaviors =
                        {
                            new EventToCommandBehavior()
                            {
                                EventName = nameof(CheckBox.CheckedChanged)
                            }
                            .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(CheckboxValidatableObject.CheckedChangedCommand)))
                        }
                    }
                    .AddBinding(CheckBox.IsCheckedProperty, new Binding(nameof(CheckboxValidatableObject.Value))),
                    new Label
                    {
                        VerticalOptions = LayoutOptions.Center
                    }
                    .AddBinding(Label.TextProperty, new Binding(nameof(CheckboxValidatableObject.CheckboxLabel)))
                }
            };

        public static Picker GetPickerForValidation()
        {
            Picker picker = new Picker()
            {
                Behaviors =
                {
                    new PickerValidationBehavior()
                        .AddBinding(PickerValidationBehavior.IsValidProperty, new Binding(nameof(PickerValidatableObject<string>.IsValid)))
                        .AddBinding(PickerValidationBehavior.IsDirtyProperty, new Binding(nameof(PickerValidatableObject<string>.IsDirty))),
                    new EventToCommandBehavior()
                    {
                        EventName = nameof(Picker.SelectedIndexChanged)
                    }
                    .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(PickerValidatableObject<string>.SelectedIndexChangedCommand)))
                }
            }
            .AddBinding(Picker.SelectedItemProperty, new Binding(nameof(PickerValidatableObject<string>.SelectedItem)))
            .AddBinding(Picker.TitleProperty, new Binding(nameof(PickerValidatableObject<string>.Title)))
            .AddBinding(Picker.ItemsSourceProperty, new Binding(nameof(PickerValidatableObject<string>.Items)));

            picker.ItemDisplayBinding = new Binding
            (
                path: ".", 
                converter: new PickerItemDisplayPathConverter(), 
                converterParameter: picker
            );

            return picker;
        }

        public static Grid GetMultiSelectFieldControl()
        {
            return new Grid
            {
                Children =
                {
                    GetEntryForMultiSelectControl(),
                    new BoxView()
                },
                GestureRecognizers =
                {
                    new TapGestureRecognizer().AddBinding
                    (
                        TapGestureRecognizer.CommandProperty,
                        new Binding(path: "OpenCommand")
                    )
                }
            };
        }

        public static DatePicker GetDatePickerForValidation()
            => new DatePicker()
            {
                Behaviors =
                {
                    new EventToCommandBehavior()
                    {
                        EventName = nameof(DatePicker.DateSelected)
                    }
                    .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(DatePickerValidatableObject.DateChangedCommand)))
                }
            }
            .AddBinding(DatePicker.DateProperty, new Binding(nameof(DatePickerValidatableObject.Value)));

        public static Entry GetEntryForMultiSelectControl()
            => GetEntry().AddBinding
            (
                Entry.TextProperty, 
                new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.DisplayText))
            );

        public static Entry GetEntryForValidation(bool isPassword = false)
            => GetEntry(isPassword).AddBinding
            (
                Entry.TextProperty, 
                new Binding(nameof(EntryValidatableObject<string>.Value))
            );

        public static Entry GetPasswordEntryForValidation()
            => GetEntryForValidation(isPassword: true);

        public static Entry GetEntry(bool isPassword = false) 
            => new Entry()
            {
                IsPassword = isPassword,
                Behaviors =
                    {
                        new EntryLineValidationBehavior()
                            .AddBinding(EntryLineValidationBehavior.IsValidProperty, new Binding(nameof(EntryValidatableObject<string>.IsValid)))
                            .AddBinding(EntryLineValidationBehavior.IsDirtyProperty, new Binding(nameof(EntryValidatableObject<string>.IsDirty))),
                        new EventToCommandBehavior()
                        {
                            EventName = nameof(Entry.TextChanged)
                        }
                        .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(EntryValidatableObject<string>.TextChangedCommand)))
                    }
            }
            .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(EntryValidatableObject<string>.Placeholder)))
            .AssignDynamicResource(VisualElement.BackgroundColorProperty, "EntryBackgroundColor")
            .AssignDynamicResource(Entry.TextColorProperty, "PrimaryTextColor");

        public static Label GetLabelForValidation() 
            => new Label
            {
                Behaviors =
                {
                    new ErrorLabelValidationBehavior()
                        .AddBinding(ErrorLabelValidationBehavior.IsValidProperty, new Binding(nameof(EntryValidatableObject<string>.IsValid)))
                        .AddBinding(ErrorLabelValidationBehavior.IsDirtyProperty, new Binding(nameof(EntryValidatableObject<string>.IsDirty)))
                }
            }
            .AddBinding(Label.TextProperty, new Binding(path: nameof(ValidatableObjectBase<object>.Errors), converter: new FirstValidationErrorConverter()))
            .AssignDynamicResource(Label.TextColorProperty, "ErrorTextColor");

        public static string GetFontAwesomeFontFamily()
            => Device.RuntimePlatform switch
            {
                Platforms.Android => FontAwesomeFontFamily.AndroidSolid,
                Platforms.iOS => FontAwesomeFontFamily.iOSSolid,
                _ => throw new ArgumentOutOfRangeException(nameof(Device.RuntimePlatform)),
            };

        private static DataTemplate GetButtonTemplate(string commandName, Type viewModelType, EventHandler buttonTappedHandler)
        {
            return new DataTemplate
            (
                () =>
                {
                    return GetButtonLayout();

                    Label GetIconLabel()
                    {
                        Label label =  new Label
                        {
                            FontSize = 18,
                            Margin = new Thickness(0, 0, 0, -3),
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Start,
                            FontFamily = GetFontAwesomeFontFamily()
                        }.AddBinding(Label.TextProperty, new Binding(path: "ButtonIcon", converter: new FontAwesomeConverter()));

                        label.SetDynamicResource(Label.TextColorProperty, "TertiaryTextColor");
                        return label;
                    }

                    Label GetTextlabel()
                    {
                        Label label =  new Label
                        {
                            FontSize = 12,
                            Margin = new Thickness(0, -1, 0, 0),
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Start,
                        }.AddBinding(Label.TextProperty, new Binding("LongString"));

                        label.SetDynamicResource(Label.TextColorProperty, "TertiaryTextColor");
                        return label;
                    }

                    TapGestureRecognizer GetTapGestureRecognizer()
                    {
                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer().AddBinding
                        (
                            TapGestureRecognizer.CommandProperty,
                            new Binding(path: commandName)
                            {
                                Source = new RelativeBindingSource
                                (
                                    RelativeBindingSourceMode.FindAncestorBindingContext,
                                    viewModelType
                                )
                            }
                        );
                        tapGestureRecognizer.Tapped += buttonTappedHandler;
                        
                        return tapGestureRecognizer;
                    }

                    StackLayout GetButtonLayout()
                    {
                        StackLayout stackLayout = new StackLayout
                        {
                            Padding = 2,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                GetIconLabel(),
                                GetTextlabel()
                            },
                            GestureRecognizers =
                            {
                                GetTapGestureRecognizer()
                            }
                        };

                        if (Application.Current.Resources.TryGetValue("SelectedCommandButtonBackgroundColor", out object backGroundColor))
                        {
                            VisualStateManager.GetVisualStateGroups(stackLayout).Add
                            (
                                new VisualStateGroup()
                                {
                                    Name = "CommonStates",
                                    States =
                                    {
                                        new VisualState { Name="Normal" },
                                        new VisualState
                                        {
                                            Name = "Selected",
                                            Setters =
                                            {
                                                new Setter
                                                {
                                                    Property = VisualElement.BackgroundColorProperty,
                                                    Value = backGroundColor
                                                }
                                            }
                                        }
                                    }
                                }
                            );
                        }

                        return stackLayout;
                    }
                }
            );
        }
    }
}
