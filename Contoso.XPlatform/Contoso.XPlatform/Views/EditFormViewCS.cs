
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(EditFormViewModelBase editFormViewModel)
        {
            this.editFormViewModel = editFormViewModel;
            AddContent(editFormViewModel.GetType());
            BindingContext = editFormViewModel;
        }

        private EditFormViewModelBase editFormViewModel;
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await page.EntranceTransition(transitionGrid, 150);
        }

        private void AddContent(Type editFormViewModelType)
        {
            transitionGrid = GetTransitionGrid();
            page = GetFieldsStackLayout();

            Content = GetFullPageGrid();

            Grid GetFullPageGrid()
            {
                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) }
                    },
                    Children =
                    {
                        page,
                        GetButtonsGrid(),
                        transitionGrid
                    }
                };

                Grid.SetRow(grid.Children[1], 1);

                return grid;
            }

            StackLayout GetFieldsStackLayout()
            {
                StackLayout stackLayout = new StackLayout
                {
                    Padding = new Thickness(30),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Children =
                    {
                        new Label
                        {
                            Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                        }
                        .AddBinding(Label.TextProperty, new Binding("FormSettings.Title")),
                        new CollectionView
                        {
                            SelectionMode = SelectionMode.Single,
                            ItemTemplate = EditFormViewHelpers.QuestionTemplateSelector
                        }
                        .AddBinding(ItemsView.ItemsSourceProperty, new Binding("Properties")),
                    }
                };

                return stackLayout;
            }

            StackLayout GetButtonsStackLayout()
            {
                StackLayout buttonsLayout = new StackLayout
                {
                    Margin = 3,
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill
                }.AddBinding(BindableLayout.ItemsSourceProperty, new Binding("Buttons"));

                BindableLayout.SetItemTemplateSelector(buttonsLayout, EditFormViewHelpers.GetCommandButtonSelector(editFormViewModelType, Button_Tapped));

                if (this.editFormViewModel.Buttons.Count > 2)
                    Grid.SetColumnSpan(buttonsLayout, 2);
                else
                    Grid.SetColumn(buttonsLayout, 1);

                return buttonsLayout;
            }

            Grid GetButtonsGrid()
            {
                Grid buttonsGrid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    Children =
                    {
                        GetButtonsStackLayout()
                    }
                };

                buttonsGrid.SetDynamicResource(VisualElement.BackgroundColorProperty, "CommandBarBackgroundColor");
                return buttonsGrid;
            }

            Grid GetTransitionGrid()
            {
                Grid grid = new Grid();
                grid.SetDynamicResource(VisualElement.BackgroundColorProperty, "PageBackgroundColor");
                return grid;
            }
        }

        private async void Button_Tapped(object sender, EventArgs e)
        {
            if (!(this.editFormViewModel).AreFieldsValid())
                return;

            StackLayout view = sender as StackLayout;
            if (view == null)
                return;

            foreach (var element in view.Children)
            {
                if (element is Label label)
                {
                    label.SetDynamicResource(Label.TextColorProperty, "PrimaryColor");
                }
            }

            await view.ScaleTo(1.1, 100);
            await view.ScaleTo(1, 100);
        }
    }
}