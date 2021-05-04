
using Contoso.Forms.Configuration;
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
            AddContent();
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

        private void AddContent()
        {
            transitionGrid = GetTransitionGrid();
            page = GetFieldsStackLayout();
            AddToolBarItems();

            Content = GetFullPageGrid();

            void AddToolBarItems()
            {
                foreach (var button in editFormViewModel.Buttons)
                    this.ToolbarItems.Add(BuildToolbarItem(button));
            }

            ToolbarItem BuildToolbarItem(CommandButtonDescriptor button)
            {
                ToolbarItem item = new ToolbarItem
                {
                    AutomationId = button.ShortString,
                    Text = button.LongString,
                    //IconImageSource = new FontImageSource
                    //{
                    //    FontFamily = EditFormViewHelpers.GetFontAwesomeFontFamily(),
                    //    Glyph = FontAwesomeIcons.Solid[button.ButtonIcon],
                    //    Size = 20
                    //},
                    Order = ToolbarItemOrder.Primary,
                    Priority = 0
                };

                AutomationProperties.SetName(item, button.ShortString);

                item.SetBinding
                (
                    MenuItem.CommandProperty,
                    new Binding(button.Command)
                );

                item.CommandParameter = button;

                return item;
            }

            Grid GetFullPageGrid()
            {
                Grid grid = new Grid
                {
                    Children =
                    {
                        page,
                        transitionGrid
                    }
                };


                return grid;
            }

            StackLayout GetFieldsStackLayout()
            {
                StackLayout stackLayout = new StackLayout
                {
                    Padding = new Thickness(30),
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