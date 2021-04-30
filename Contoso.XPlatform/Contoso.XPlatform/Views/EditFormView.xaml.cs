using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFormView : ContentPage
    {
        public EditFormView(object editFormViewModel)
        {
            InitializeComponent();
            this.BindingContext = editFormViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await page.EntranceTransition(transitionGrid, 150);
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