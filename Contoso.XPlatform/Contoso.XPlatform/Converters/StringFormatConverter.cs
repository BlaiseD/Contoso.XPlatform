using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Contoso.XPlatform.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetFormattedString(GetFormControlSettingsDescriptor());

            object GetFormattedString(FormControlSettingsDescriptor formControlSettings)
            {
                if (value == null)
                    return null;

                if (string.IsNullOrEmpty(formControlSettings.StringFormat))
                    return value;

                return string.Format(CultureInfo.CurrentCulture, formControlSettings.StringFormat, value);
            }

            FormControlSettingsDescriptor GetFormControlSettingsDescriptor()
                => ((VisualElement)parameter).BindingContext.GetPropertyValue<FormControlSettingsDescriptor>
                (
                    nameof(EntryValidatableObject<string>.FormControlSettingsDescriptor)
                );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().TryParse(targetType, out object result))
                return result;

            return value;
        }
    }
}
