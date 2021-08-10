using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.EditForm;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFormView : ContentPage
    {
        public EditFormView(EditFormViewModel editFormViewModel)
        {
            this.editFormEntityViewModel = editFormViewModel.EditFormEntityViewModel;
            InitializeComponent();
            AddToolBarItems();
            Title = this.editFormEntityViewModel.FormSettings.Title;
            void AddToolBarItems()
            {
                foreach (var button in this.editFormEntityViewModel.Buttons)
                    this.ToolbarItems.Add(BuildToolbarItem(button));
            }

            ToolbarItem BuildToolbarItem(CommandButtonDescriptor button)
                => new ToolbarItem
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
                    Priority = 0,
                    CommandParameter = button
                }
                .AddBinding(MenuItem.CommandProperty, new Binding(button.Command))
                .SetAutomationPropertiesName(button.ShortString);
            this.BindingContext = this.editFormEntityViewModel;
        }

        public EditFormEntityViewModelBase editFormEntityViewModel { get; set; }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }
    }
}