
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

        private void AddContent(Type editFormViewModelType)
        {
            StackLayout buttonsLayout = new StackLayout
            {
                Padding = new Thickness(3),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill
            };

            Grid grid = new Grid
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
                        Children = {
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
                    buttonsLayout.AddBinding(BindableLayout.ItemsSourceProperty, new Binding("Buttons"))
                }
            };

            buttonsLayout.SetDynamicResource(VisualElement.BackgroundColorProperty, "ResultListBackgroundColor");
            BindableLayout.SetItemTemplateSelector(buttonsLayout, EditFormViewHelpers.GetCommandButtonSelector(editFormViewModelType));

            Grid.SetRow(grid.Children[0], 0);
            Grid.SetRow(grid.Children[1], 1);

            Content = grid;
        }
    }
}