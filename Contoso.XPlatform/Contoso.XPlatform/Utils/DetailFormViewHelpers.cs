﻿using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    public static class DetailFormViewHelpers
    {
        public static ReadOnlyControlTemplateSelector ReadOnlyControlTemplateSelector => new ReadOnlyControlTemplateSelector
        {
            CheckboxTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetTextFieldControl()
                    }
                }
            ),
            DateTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetTextFieldControl()
                    }
                }
            ),
            FormGroupArrayTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetPopupFormArrayFieldControl()
                    }
                }
            ),
            HiddenTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    IsVisible = false,
                    HeightRequest = 1
                }
            ),
            MultiSelectTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetMultiSelectFieldControl()
                    }
                }
            ),
            PasswordTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetPasswordTextFieldControl()
                    }
                }
            ),
            PopupFormGroupTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetPopupFormFieldControl()
                    }
                }
            ),
            PickerTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetTextFieldControl()
                    }
                }
            ),
            TextTemplate = new DataTemplate
            (
                () => new StackLayout
                {
                    Children =
                    {
                        GetTextFieldControl()
                    }
                }
            )
        };

        private static Grid GetTextFieldControl() 
            => new Grid
            {
                Children =
                {
                    GetTextField
                    (
                        nameof(TextFieldReadOnlyObject<string>.Title), 
                        nameof(TextFieldReadOnlyObject<string>.DisplayText)
                    )
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(TextFieldReadOnlyObject<string>.Placeholder))),
                    new BoxView()
                }
            };

        private static Grid GetPasswordTextFieldControl()
            => new Grid
            {
                Children =
                {
                    GetPasswordTextField
                    (
                        nameof(TextFieldReadOnlyObject<string>.Title),
                        nameof(TextFieldReadOnlyObject<string>.DisplayText)
                    )
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(TextFieldReadOnlyObject<string>.Placeholder))),
                    new BoxView()
                }
            };

        private static Grid GetMultiSelectFieldControl()
            => new Grid
            {
                Children =
                {
                    GetEntry()
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.DisplayText)))
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Placeholder))),
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

        private static Grid GetPopupFormFieldControl() 
            => new Grid
            {
                Children =
                {
                    GetEntry()
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(FormReadOnlyObject<string>.Placeholder))),
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

        private static Grid GetPopupFormArrayFieldControl()
            => new Grid
            {
                Children =
                {
                    GetEntry()
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.Placeholder))),
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

        private static View GetPasswordTextField(string titleBinding, string valueBinding)
            => GetTextField(titleBinding, valueBinding, isPassword: true);

        private static View GetTextField(string titleBinding, string valueBinding, bool isPassword = false)
            => new Label
            {
                Style = LayoutHelpers.GetStaticStyleResource("DetailFormLabel"),
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span{ FontAttributes = FontAttributes.Italic }.AddBinding(Span.TextProperty, new Binding(titleBinding)),
                        new Span { Text = ":  " },
                        isPassword
                            ? new Span{ FontAttributes = FontAttributes.Bold, Text = "*****" }
                            : new Span{ FontAttributes = FontAttributes.Bold }.AddBinding(Span.TextProperty, new Binding(valueBinding))
                    }
                }
            };

        static Entry GetEntry()
            => new Entry()
            .AssignDynamicResource(VisualElement.BackgroundColorProperty, "EntryBackgroundColor")
            .AssignDynamicResource(Entry.TextColorProperty, "PrimaryTextColor");
    }
}
