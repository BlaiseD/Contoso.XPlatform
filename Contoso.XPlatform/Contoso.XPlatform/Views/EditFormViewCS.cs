using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.EditForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Linq;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(EditFormViewModel editFormViewModel)
        {
            this.editFormEntityViewModel = editFormViewModel.EditFormEntityViewModel;
            AddContent();
            BindingContext = this.editFormEntityViewModel;
        }

        private EditFormEntityViewModelBase editFormEntityViewModel;
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
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.editFormEntityViewModel.Buttons);
            Title = editFormEntityViewModel.FormSettings.Title;

            BindingBase GetHeaderBinding(MultiBindingDescriptor multiBindingDescriptor)
            {
                if (editFormEntityViewModel.FormSettings.EditType == EditType.Add 
                    || multiBindingDescriptor == null)
                    return new Binding($"{nameof(EditFormEntityViewModelBase.FormSettings)}.{nameof(EditFormSettingsDescriptor.Title)}");

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(EditFormEntityViewModel<Domain.ViewModelBase>.PropertiesDictionary)}[{field}].{nameof(IValidatable.Value)}")
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
                                    GetHeaderBinding(editFormEntityViewModel.FormSettings.HeaderBindings)
                                ),
                                new CollectionView
                                {
                                    SelectionMode = SelectionMode.None,
                                    ItemTemplate = EditFormViewHelpers.QuestionTemplateSelector
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(EditFormEntityViewModelBase.Properties))),
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