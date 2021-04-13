
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(EditFormViewModel editFormViewModel)
        {
            AddContent();
            BindingContext = editFormViewModel;
        }

        private void AddContent()
        {
            StackLayout stackLayout = new StackLayout
            {
                Padding = new Thickness(30),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    new Label { Text = "Create account", Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle") },
                    new CollectionView
                    {
                        SelectionMode = SelectionMode.Single,
                        ItemTemplate = EditFormViewHelpers.QuestionTemplateSelector
                    }
                    .AddBinding(ItemsView.ItemsSourceProperty, new Binding("Properties")),
                    new Button
                    {
                        Text = "CREATE ACCOUNT"
                    }
                    .AddBinding(Button.CommandProperty, new Binding("SignUpCommand"))
                }
            };

            Content = stackLayout;
        }
    }
}