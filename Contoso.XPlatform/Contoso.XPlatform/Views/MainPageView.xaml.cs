using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Navigation;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : FlyoutPage
    {
        public MainPageView()
        {
            InitializeComponent();
            flyout.ListView.SelectionChanged += ListView_SelectionChanged;
            FlyoutLayoutBehavior = FlyoutLayoutBehavior.SplitOnLandscape;
            ViewModel = App.ServiceProvider.GetRequiredService<MainPageViewModel>();
            this.BindingContext = ViewModel;
            flyout.BindingContext = ViewModel;
            FlowSettingsChanged(Descriptors.GetFlowSettings<EditFormSettingsDescriptor>("home"));
        }

        #region Properties
        public MainPageViewModel ViewModel { get; }
        private bool IsPortrait => Width < Height;
        #endregion Properties

        #region Methods
        protected override void OnAppearing()
        {
            
            base.OnAppearing();
        }

        private void FlowSettingsChanged(FlowSettings flowSettings)
        {
            flowSettings.FlowDataCache.NavigationBar.MenuItems
                .ForEach(item => item.Active = item.InitialModule == flowSettings.FlowDataCache.NavigationBar.CurrentModule);

            /*To Remove*/
            if (flowSettings.ScreenSettings == null)
                return;

            ChangePage(flowSettings.ScreenSettings.CreatePage());

            UpdateNavigationMenu(flowSettings);
        }

        private void UpdateNavigationMenu(FlowSettings flowSettings)
            => ViewModel.MenuItems = new ObservableCollection<NavigationMenuItemDescriptor>(flowSettings.FlowDataCache.NavigationBar.MenuItems);

        private void ChangePage(Page page)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => Detail = GetNavigationPage(page)
            );

            if (IsPortrait)
                IsPresented = false;

            flyout.ListView.SelectedItem = null;
        }
        #endregion Methods

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 1)
                return;

            if (!(e.CurrentSelection.First() is NavigationMenuItemDescriptor item))
                return;
            if (item.Active)
                return;

            DisposeCurrentPageBindingContext(Detail);

            FlowSettingsChanged(Descriptors.GetFlowSettings<EditFormSettingsDescriptor>(item.InitialModule));

            void DisposeCurrentPageBindingContext(Page detail)
            {
                if (detail is not NavigationPage navigationPage)
                    return;

                if (navigationPage?.RootPage.BindingContext is not IDisposable disposable)
                    return;

                disposable.Dispose();
            }
        }

        private NavigationPage GetNavigationPage(Page page)
        {
            NavigationPage.SetHasBackButton(page, false);
            page.SetDynamicResource(Page.BackgroundColorProperty, "PageBackgroundColor");
            var navigationPage = new NavigationPage(page);
            navigationPage.SetDynamicResource(NavigationPage.BarBackgroundColorProperty, "PageBackgroundColor");
            navigationPage.SetDynamicResource(NavigationPage.BarTextColorProperty, "PrimaryTextColor");

            return navigationPage;
        }
    }
}