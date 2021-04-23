using Contoso.XPlatform.Views;
using Microsoft.Extensions.DependencyInjection;
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

        #region Properties
        public static IServiceProvider ServiceProvider { get; set; }
        public static ServiceCollection ServiceCollection { get; set; }
        #endregion Properties

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
