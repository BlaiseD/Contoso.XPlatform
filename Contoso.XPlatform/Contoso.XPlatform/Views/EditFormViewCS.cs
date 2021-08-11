
using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.EditForm;
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
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        private void AddContent()
        {
            AddToolBarItems();

            Title = editFormEntityViewModel.FormSettings.Title;
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
                                .AddBinding(Label.TextProperty, new Binding("FormSettings.Title")),
                                new CollectionView
                                {
                                    SelectionMode = SelectionMode.Single,
                                    ItemTemplate = EditFormViewHelpers.QuestionTemplateSelector
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding("Properties")),
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
                foreach (var button in editFormEntityViewModel.Buttons)
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
    }
}