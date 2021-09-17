using Contoso.XPlatform.ViewModels.ReadOnlys;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class ReadOnlyChildFormPageCS : ContentPage
    {
        public ReadOnlyChildFormPageCS(IReadOnly formValidatable)
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
    }
}