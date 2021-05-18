using Contoso.XPlatform.ViewModels.Validatables;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    public class QuestionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate PasswordTemplate { get; set; }
        public DataTemplate DateTemplate { get; set; }
        public DataTemplate CheckboxTemplate { get; set; }
        public DataTemplate PickerTemplate { get; set; }
        public DataTemplate MultiSelectTemplate { get; set; }
        public DataTemplate PopupFormGroupTemplate { get; set; }
        public DataTemplate FormGroupArrayTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            IValidatable input = (IValidatable)item;

            return input.TemplateName switch
            {
                nameof(DateTemplate) => DateTemplate,
                nameof(CheckboxTemplate) => CheckboxTemplate,
                nameof(PasswordTemplate) => PasswordTemplate,
                nameof(PickerTemplate) => PickerTemplate,
                nameof(MultiSelectTemplate) => MultiSelectTemplate,
                nameof(PopupFormGroupTemplate) => PopupFormGroupTemplate,
                nameof(FormGroupArrayTemplate) => FormGroupArrayTemplate,
                _ => TextTemplate,
            };
        }
    }
}
