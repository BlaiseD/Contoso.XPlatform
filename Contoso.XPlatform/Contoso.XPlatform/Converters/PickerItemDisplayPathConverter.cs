using Contoso.Forms.Configuration;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Contoso.XPlatform.Converters
{
    public class PickerItemDisplayPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            object bindingContext = ((VisualElement)parameter).BindingContext;
            var dropDownTemplate = (DropDownTemplateDescriptor)bindingContext.GetType().GetProperty
            (
                nameof(PickerValidatableObject<int>.DropDownTemplate)
            ).GetValue(bindingContext);

            return (string)value.GetType().GetProperty(dropDownTemplate.TextField).GetValue(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
