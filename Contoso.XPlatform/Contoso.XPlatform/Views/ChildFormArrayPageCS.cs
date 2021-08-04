using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class ChildFormArrayPageCS : ContentPage
    {
        public ChildFormArrayPageCS(IValidatable formArrayValidatable)
        {
            this.formArrayValidatable = formArrayValidatable;
            this.formsCollectionDisplayTemplateDescriptor = (FormsCollectionDisplayTemplateDescriptor)this.formArrayValidatable.GetType()
                .GetProperty(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.FormsCollectionDisplayTemplate))
                .GetValue(this.formArrayValidatable);

            Content = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    new ContentView
                    {
                        Content = new StackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource("FormArrayPopupViewStyle"),
                            Children =
                            {
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupHeaderStyle"),
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupHeaderLabelStyle"),
                                        }.AddBinding(Label.TextProperty, new Binding("Title"))
                                    }
                                },
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("FormArrayPopupCollectionViewStyle"),
                                    ItemTemplate = GetCollectionViewItemTemplate()
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding("Entities")),
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource("PopupFooterSeparatorStyle") },
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupFooterStyle"),
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                                    },
                                    Children =
                                    {
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupEditButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("EditCommand")),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupCancelButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("CancelCommand"))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupAcceptButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("SubmitCommand"))
                                        .SetGridColumn(2)
                                    }
                                }
                            }
                        }
                    }
                    .AssignDynamicResource(VisualElement.BackgroundColorProperty, "PopupViewBackgroundColor")
                    .SetAbsoluteLayoutBounds(new Rectangle(0, 0, 1, 1))
                    .SetAbsoluteLayoutFlags(AbsoluteLayoutFlags.All)
                }
            };

            this.BackgroundColor = Color.Transparent;
            this.BindingContext = this.formArrayValidatable;
        }

        private IValidatable formArrayValidatable;
        private FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;

        private struct TemplateNames
        {
            public const string TextDetailTemplate = "TextDetailTemplate";
            public const string HeaderTextDetailTemplate = "HeaderTextDetailTemplate";
        }

        private struct BindingNames
        {
            public const string Header = "Header";
            public const string Text = "Text";
            public const string Detail = "Detail";
        }

        private DataTemplate GetCollectionViewItemTemplate()
        {
            switch (formsCollectionDisplayTemplateDescriptor.TemplateName)
            {
                case TemplateNames.HeaderTextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Padding = new Thickness(10),
                            HeightRequest = 60,
                            Children =
                            {
                                new Label
                                {
                                    FontAttributes = FontAttributes.Bold
                                }
                                .AddBinding
                                (
                                    Label.TextProperty, 
                                    new Binding
                                    (
                                        formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Header].Name,
                                        stringFormat : formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Header].StringFormat 
                                    )
                                ),
                                new Label
                                { 
                                    VerticalOptions = LayoutOptions.Center 
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    new Binding
                                    (
                                        formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Text].Name,
                                        stringFormat : formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Text].StringFormat
                                    )
                                ),
                                new Label
                                {
                                    FontAttributes = FontAttributes.Italic,
                                    VerticalOptions = LayoutOptions.End
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    new Binding
                                    (
                                        formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Detail].Name,
                                        stringFormat : formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Detail].StringFormat
                                    )
                                )
                            }
                        }
                    );
                case TemplateNames.TextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Padding = new Thickness(10),
                            HeightRequest = 60,
                            Children =
                            {
                                new Label
                                {
                                    FontAttributes = FontAttributes.Bold
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    new Binding
                                    (
                                        formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Text].Name,
                                        stringFormat : formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Text].StringFormat
                                    )
                                ),
                                new Label
                                {
                                    FontAttributes = FontAttributes.Italic,
                                    VerticalOptions = LayoutOptions.End
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    new Binding
                                    (
                                        formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Detail].Name,
                                        stringFormat : formsCollectionDisplayTemplateDescriptor.Bindings[BindingNames.Detail].StringFormat
                                    )
                                )
                            }
                        }
                    );
                default:
                    throw new ArgumentException
                    (
                        $"{nameof(formsCollectionDisplayTemplateDescriptor.TemplateName)}: 02A779FF-7872-4BD3-B85C-19B79827D926"
                    );
            }
        }
    }
}