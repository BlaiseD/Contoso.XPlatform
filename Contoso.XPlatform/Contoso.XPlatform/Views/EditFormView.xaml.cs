using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
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
            InitializeComponent();
            this.BindingContext = editFormViewModel.EditFormEntityViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }
    }
}