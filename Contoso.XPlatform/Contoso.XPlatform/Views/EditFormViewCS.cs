
using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.EditForm;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(EditFormViewModel editFormViewModel)
        {
            this.editFormEntityViewModel = editFormViewModel.EditFormEntityViewModel;
            AddContent();
            BindingContext = this.editFormEntityViewModel;
        }

        private EditFormEntityViewModelBase editFormEntityViewModel;
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

            Title = editFormEntityViewModel.FormSettings.Title;
            Content = GetFullPageGrid();

            void AddToolBarItems()
            {
                foreach (var button in editFormEntityViewModel.Buttons)
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
                => new Grid
                {
                    Children =
                    {
                        page,
                        transitionGrid
                    }
                };

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
    }
}