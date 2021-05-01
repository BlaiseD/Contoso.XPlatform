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
        public EditFormView(EditFormViewModelBase editFormViewModel)
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
            if (!((EditFormViewModelBase)this.BindingContext).AreFieldsValid())
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