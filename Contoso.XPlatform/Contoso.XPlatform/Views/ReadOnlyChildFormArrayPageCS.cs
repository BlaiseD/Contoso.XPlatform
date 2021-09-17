using Contoso.XPlatform.ViewModels.ReadOnlys;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class ReadOnlyChildFormArrayPageCS : ContentPage
    {
        public ReadOnlyChildFormArrayPageCS(IReadOnly formArrayValidatable)
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