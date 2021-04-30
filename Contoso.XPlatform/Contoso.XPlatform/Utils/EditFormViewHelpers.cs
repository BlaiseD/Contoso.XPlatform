using Contoso.XPlatform.Behaviours;
using Contoso.XPlatform.Converters;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    internal static class EditFormViewHelpers
    {
        public static CommandButtonSelector GetCommandButtonSelector(Type viewModelType) => new CommandButtonSelector
        {
            SubmitButtonTemplate = GetButtonTemplate("SubmitCommand", viewModelType),
            NavigateButtonTemplate = GetButtonTemplate("NavigateCommand", viewModelType)
        };

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
            => new Picker()
            {
                Behaviors =
                {
                    new EventToCommandBehavior()
                    {
                        EventName = nameof(Picker.SelectedIndexChanged)
                    }
                    .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(PickerValidatableObject<string>.SelectedIndexChangedCommand)))
                }
            }
            .AddBinding(Picker.SelectedItemProperty, new Binding(nameof(PickerValidatableObject<string>.Value)))
            .AddBinding(Picker.TitleProperty, new Binding(nameof(PickerValidatableObject<string>.Title)))
            .AddBinding(Picker.ItemsSourceProperty, new Binding(nameof(PickerValidatableObject<string>.Items)));

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

        public static Entry GetEntryForValidation(bool isPassword = false)
        {
            Entry entry = new Entry()
            {
                IsPassword = isPassword,
                Behaviors =
                {
                    new EntryLineValidationBehaviour()
                        .AddBinding(EntryLineValidationBehaviour.IsValidProperty, new Binding(nameof(EntryValidatableObject.IsValid))),
                    new EventToCommandBehavior()
                    {
                        EventName = nameof(Entry.TextChanged)
                    }
                    .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(EntryValidatableObject.TextChangedCommand)))
                }
            }
            .AddBinding(Entry.TextProperty, new Binding(nameof(EntryValidatableObject.Value)))
            .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(EntryValidatableObject.Placeholder)));

            entry.SetDynamicResource(VisualElement.BackgroundColorProperty, "EntryBackgroundColor");
            entry.SetDynamicResource(Entry.TextColorProperty, "PrimaryTextColor");

            return entry;
        }

        public static Entry GetPasswordEntryForValidation()
            => GetEntryForValidation(isPassword: true);

        public static Label GetLabelForValidation()
        {
            Label label = new Label()
            .AddBinding(Label.TextProperty, new Binding(path: nameof(ValidatableObjectBase<object>.Errors), converter: new FirstValidationErrorConverter()))
            .AddBinding(Label.IsVisibleProperty, new Binding(path: nameof(ValidatableObjectBase<object>.IsValid), converter: new InverseBoolConverter()));

            label.SetDynamicResource(Label.TextColorProperty, "ErrorTextColor");
            return label;
        }

        private static string GetFontAwesomeFontFamily()
            => Device.RuntimePlatform switch
            {
                Platforms.Android => FontAwesomeFontFamily.AndroidSolid,
                Platforms.iOS => FontAwesomeFontFamily.iOSSolid,
                _ => throw new ArgumentOutOfRangeException(nameof(Device.RuntimePlatform)),
            };

        private static DataTemplate GetButtonTemplate(string commandName, Type viewModelType)
        {
            return new DataTemplate
            (
                () =>
                {
                    Label iconLabel = new Label
                    {
                        TextColor = Color.WhiteSmoke,
                        FontSize = 18,
                        HorizontalOptions = LayoutOptions.Center,
                        FontFamily = GetFontAwesomeFontFamily()
                    }.AddBinding(Label.TextProperty, new Binding(path: "ButtonIcon", converter: new FontAwesomeConverter()));
                    iconLabel.SetDynamicResource(Label.TextColorProperty, "PrimaryTextColor");

                    Label textLabel = new Label
                    {
                        TextColor = Color.WhiteSmoke,
                        FontSize = 12,
                        HorizontalOptions = LayoutOptions.Center,
                    }.AddBinding(Label.TextProperty, new Binding("LongString"));
                    iconLabel.SetDynamicResource(Label.TextColorProperty, "PrimaryTextColor");
                    iconLabel.SetDynamicResource(VisualElement.StyleProperty, "ListItemTextStyle");

                    StackLayout grid = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            iconLabel,
                            textLabel
                        },
                        GestureRecognizers =
                        {
                            new TapGestureRecognizer().AddBinding
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
                            )
                        }
                    };

                    if (!Application.Current.Resources.TryGetValue("SelectedCommandButtonBackgroundColor", out object backGroundColor))
                        throw new ArgumentException("SelectedCommandButtonBackgroundColor: FB3FDD7A-3554-402A-A4C3-DE35723FD3B3");

                    VisualStateManager.GetVisualStateGroups(grid).Add
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

                    return grid;
                }
            );
        }
    }
}
