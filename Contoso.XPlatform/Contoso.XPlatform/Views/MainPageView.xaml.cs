using Contoso.Forms.Configuration.EditForm;
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
    public partial class MainPageView : FlyoutPage
    {
        public MainPageView()
        {
            InitializeComponent();
            flyout.ListView.SelectionChanged += ListView_SelectionChanged;
            FlowSettingsChanged();
        }

        private void FlowSettingsChanged()
        {
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 1)
                return;

            if (!(e.CurrentSelection.First() is MainPageViewMasterMenuItem item))
                return;
            Page page;
            if (item.TargetType == typeof(EditFormViewCS))
            {
                page = new EditFormViewCS(new EditFormViewModel(Descriptors.StudentForm));
            }
            else if(item.TargetType == typeof(EditFormView))
            {
                page = new EditFormView(new EditFormViewModel(Descriptors.StudentForm));
            }
            else
            {
                page = (Page)Activator.CreateInstance(item.TargetType);
            }
            
            page.Title = item.Title;

            Detail = GetNavigationPage(page);
            IsPresented = false;

            flyout.ListView.SelectedItem = null;
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