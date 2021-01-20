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
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 1)
                return;

            if (!(e.CurrentSelection.First() is MainPageViewMasterMenuItem item))
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            flyout.ListView.SelectedItem = null;
        }
    }
}