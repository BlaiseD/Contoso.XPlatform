using Contoso.Forms.Configuration;
using Contoso.XPlatform.Behaviours;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.SearchPage;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class SearchPageViewCS : ContentPage
    {
        public SearchPageViewCS(SearchPageViewModel searchPageViewModel)
        {
            this.searchPageListViewModel = searchPageViewModel.SearchPageEntityViewModel;
            AddContent();
            BindingContext = this.searchPageListViewModel;
        }

        public SearchPageListViewModelBase searchPageListViewModel { get; set; }
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        private void AddContent()
        {
            AddToolBarItems();
            Title = searchPageListViewModel.FormSettings.Title;

            Content = new Grid
            {
                Children =
                {
                    (
                        page = new StackLayout
                        {
                            Padding = new Thickness(30),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                                }
                                .AddBinding(Label.TextProperty, new Binding(nameof(SearchPageListViewModelBase.Title))),
                                new SearchBar
                                {
                                    Behaviors =
                                    {
                                        new EventToCommandBehavior
                                        {
                                            EventName = nameof(SearchBar.TextChanged),
                                        }
                                        .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(SearchPageListViewModel<Domain.ViewModelBase>.TextChangedCommand)))
                                    }
                                }
                                .AddBinding(SearchBar.TextProperty, new Binding(nameof(SearchPageListViewModel<Domain.ViewModelBase>.SearchText)))
                                .AddBinding(SearchBar.PlaceholderProperty, new Binding(nameof(SearchPageListViewModelBase.FilterPlaceholder))),
                                new RefreshView
                                {
                                    Content = new CollectionView
                                    {
                                        Style = LayoutHelpers.GetStaticStyleResource("SearchFormCollectionViewStyle"),
                                        ItemTemplate = GetCollectionViewItemTemplate()
                                    }
                                    .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(SearchPageListViewModel<Domain.ViewModelBase>.Items)))
                                    .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(SearchPageListViewModel<Domain.ViewModelBase>.SelectionChangedCommand)))
                                    .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(SearchPageListViewModel<Domain.ViewModelBase>.SelectedItem)))
                                }
                                .AddBinding(RefreshView.IsRefreshingProperty, new Binding(nameof(SearchPageListViewModel<Domain.ViewModelBase>.IsRefreshing)))
                                .AddBinding(RefreshView.CommandProperty, new Binding(nameof(SearchPageListViewModel<Domain.ViewModelBase>.RefreshCommand)))
                            }
                        }
                    ),
                    (
                        transitionGrid = new Grid().AssignDynamicResource
                        (
                            VisualElement.BackgroundColorProperty,
                            "PageBackgroundColor"
                        )
                    )
                }
            };

            void AddToolBarItems()
            {
                foreach (var button in this.searchPageListViewModel.Buttons)
                    this.ToolbarItems.Add(BuildToolbarItem(button));
            }

            ToolbarItem BuildToolbarItem(CommandButtonDescriptor button)
                => new ToolbarItem
                {
                    AutomationId = button.ShortString,
                    //Text = button.LongString,
                    IconImageSource = new FontImageSource
                    {
                        FontFamily = EditFormViewHelpers.GetFontAwesomeFontFamily(),
                        Glyph = FontAwesomeIcons.Solid[button.ButtonIcon],
                        Size = 20
                    },
                    Order = ToolbarItemOrder.Primary,
                    Priority = 0,
                    CommandParameter = button
                }
                .AddBinding(MenuItem.CommandProperty, new Binding(button.Command))
                .SetAutomationPropertiesName(button.ShortString);
        }

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
            switch (this.searchPageListViewModel.FormSettings.ItemTemplateName)
            {
                case TemplateNames.HeaderTextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource("SearchFormItemStyle"),
                            Children =
                            {
                                new StackLayout
                                {
                                    Margin = new Thickness(2),
                                    Padding = new Thickness(7),
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
                                                this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Header].Property,
                                                stringFormat : this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Header].StringFormat
                                            )
                                        ),
                                        new Label
                                        {
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Text].Property,
                                                stringFormat : this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Text].StringFormat
                                            )
                                        ),
                                        new Label
                                        {
                                            FontAttributes = FontAttributes.Italic
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Detail].Property,
                                                stringFormat : this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Detail].StringFormat
                                            )
                                        )
                                    }
                                }
                                .AssignDynamicResource(VisualElement.BackgroundColorProperty, "ResultListBackgroundColor")
                            }
                        }
                    );
                case TemplateNames.TextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource("SearchFormItemStyle"),
                            Children =
                            {
                                new StackLayout
                                {
                                    Margin = new Thickness(2),
                                    Padding = new Thickness(7),
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
                                                this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Text].Property,
                                                stringFormat : this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Text].StringFormat
                                            )
                                        ),
                                        new Label
                                        {
                                            FontAttributes = FontAttributes.Italic,
                                            VerticalOptions = LayoutOptions.Center
                                        }
                                        .AddBinding
                                        (
                                            Label.TextProperty,
                                            new Binding
                                            (
                                                this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Detail].Property,
                                                stringFormat : this.searchPageListViewModel.FormSettings.Bindings[BindingNames.Detail].StringFormat
                                            )
                                        )
                                    }
                                }
                                .AssignDynamicResource(VisualElement.BackgroundColorProperty, "ResultListBackgroundColor")
                            }
                        }
                    );
                default:
                    throw new ArgumentException
                    (
                        $"{nameof(this.searchPageListViewModel.FormSettings.ItemTemplateName)}: 83C55FEE-9A93-45D3-A972-2335BA0F16AE"
                    );
            }
        }
    }
}