
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusyIndicator : ContentPage
    {
        public BusyIndicator()
        {
            InitializeComponent();
            this.BackgroundColor = Color.Transparent;
        }
    }
}