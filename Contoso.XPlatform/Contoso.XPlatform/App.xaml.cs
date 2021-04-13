using Contoso.XPlatform.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPageView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
