using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.DetailForm;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Linq;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class DetailFormViewCS : ContentPage
    {
        public DetailFormViewCS(DetailFormViewModel detailFormViewModel)
        {
            this.detailFormEntityViewModel = detailFormViewModel.DetailFormEntityViewModel;
            AddContent();
            BindingContext = this.detailFormEntityViewModel;
        }

        private DetailFormEntityViewModelBase detailFormEntityViewModel;
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.detailFormEntityViewModel.Buttons);
            Title = detailFormEntityViewModel.FormSettings.Title;

            BindingBase GetHeaderBinding(string stringFormat)
            {
                if (detailFormEntityViewModel.FormSettings.HeaderBindings == null)
                    return new Binding($"{nameof(DetailFormEntityViewModelBase.FormSettings)}.{nameof(DetailFormSettingsDescriptor.Title)}");

                return new MultiBinding
                {
                    StringFormat = stringFormat,
                    Bindings = detailFormEntityViewModel.FormSettings.HeaderBindings.Fields.Select
                    (
                        field => new Binding($"{nameof(DetailFormEntityViewModel<Domain.ViewModelBase>.PropertiesDictionary)}[{field}].{nameof(IReadOnly.Value)}")
                    )
                    .Cast<BindingBase>()
                    .ToList()
                };
            }

            Content = new Grid
            {
                Children =
                {
                    (
                        page = new StackLayout
                        {
                            Padding = new Thickness(30),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    GetHeaderBinding(detailFormEntityViewModel.FormSettings.HeaderBindings.HeaderStringFormat)
                                ),
                                new Label
                                {
                                    IsVisible = detailFormEntityViewModel.FormSettings.DetailType == DetailType.Delete,
                                    Style = LayoutHelpers.GetStaticStyleResource("DetailFormDeleteQuestionStyle")
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    GetHeaderBinding(detailFormEntityViewModel.FormSettings.HeaderBindings.SubTitleStringFormat)
                                ),
                                new CollectionView
                                {
                                    SelectionMode = SelectionMode.None,
                                    ItemTemplate = DetailFormViewHelpers.ReadOnlyControlTemplateSelector
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(DetailFormEntityViewModelBase.Properties))),
                            }
                        }
                    ),
                    (
                        transitionGrid = new Grid().AssignDynamicResource
                        (
                            VisualElement.BackgroundColorProperty,
                            "PageBackgroundColor"
                        )
                    )
                }
            };
        }
    }
}