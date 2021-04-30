
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(object editFormViewModel)
        {
            AddContent(editFormViewModel.GetType());
            BindingContext = editFormViewModel;
        }

        private Grid transitionGrid;
        private Grid page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await page.EntranceTransition(transitionGrid, 150);
        }

        private void AddContent(Type editFormViewModelType)
        {
            StackLayout buttonsLayout = new StackLayout
            {
                Padding = new Thickness(3),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill
            };

            buttonsLayout.SetDynamicResource(VisualElement.BackgroundColorProperty, "CommandBarBackgroundColor");
            BindableLayout.SetItemTemplateSelector(buttonsLayout, EditFormViewHelpers.GetCommandButtonSelector(editFormViewModelType, Button_Tapped));

            transitionGrid = new Grid();
            transitionGrid.SetDynamicResource(VisualElement.BackgroundColorProperty, "PageBackgroundColor");

            page = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) }
                },
                Children =
                {
                    new StackLayout
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
                    },
                    buttonsLayout.AddBinding(BindableLayout.ItemsSourceProperty, new Binding("Buttons")),
                    transitionGrid
                }
            };

            Grid.SetRow(page.Children[0], 0);
            Grid.SetRow(page.Children[1], 1);

            Content = page;
        }

        private async void Button_Tapped(object sender, EventArgs e)
        {
            StackLayout view = sender as StackLayout;
            if (view == null)
                return;

            await view.ScaleTo(1.1, 100);
            await view.ScaleTo(1, 100);
        }
    }
}