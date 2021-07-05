﻿using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class MultiSelectPageCS : ContentPage
    {
        public MultiSelectPageCS(IValidatable multiSelectValidatable)
        {
            this.multiSelectValidatable = multiSelectValidatable;
            this.multiSelectTemplateDescriptor = (MultiSelectTemplateDescriptor)this.multiSelectValidatable.GetType()
                .GetProperty(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.MultiSelectTemplate))
                .GetValue(this.multiSelectValidatable);

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
                            Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupViewStyle"),
                            Children =
                            {
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupHeaderStyle"),
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupHeaderLabelStyle"),
                                        }.AddBinding(Label.TextProperty, new Binding("Title"))
                                    }
                                },
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupCollectionViewStyle"),
                                    ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding("Items"))
                                .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding("SelectedItems")),
                                new Grid
                                {
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
                                            Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupCancelButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("CancelCommand"))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupAcceptButtonStyle")
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

            this.BindingContext = this.multiSelectValidatable;
        }

        private IValidatable multiSelectValidatable;
        private MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}