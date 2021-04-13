using Contoso.XPlatform.Validators;
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

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            IValidatable input = (IValidatable)item;

            switch (input.TemplateName)
            {
                case nameof(DateTemplate):
                    return DateTemplate;
                case nameof(CheckboxTemplate):
                    return CheckboxTemplate;
                case nameof(PasswordTemplate):
                    return PasswordTemplate;
                case nameof(PickerTemplate):
                    return PickerTemplate;
                default:
                    return TextTemplate;
            }
        }
    }
}
