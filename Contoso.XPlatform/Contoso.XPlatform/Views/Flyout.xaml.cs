using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Flyout : ContentPage
    {
        public CollectionView ListView;

        public Flyout()
        {
            InitializeComponent();

            BindingContext = new MainPageViewMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageViewMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageViewMasterMenuItem> MenuItems { get; set; }

            public MainPageViewMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageViewMasterMenuItem>(new[]
                {
                    new MainPageViewMasterMenuItem { Id = 0, Title = "Page 1", TargetType = typeof(MainPageViewDetail) },
                    new MainPageViewMasterMenuItem { Id = 1, Title = "Page 2", TargetType = typeof(EditFormViewCS) },
                    new MainPageViewMasterMenuItem { Id = 2, Title = "Page 3", TargetType = typeof(MainPageViewDetail) },
                    new MainPageViewMasterMenuItem { Id = 3, Title = "Page 4", TargetType = typeof(EditFormView) },
                    new MainPageViewMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}