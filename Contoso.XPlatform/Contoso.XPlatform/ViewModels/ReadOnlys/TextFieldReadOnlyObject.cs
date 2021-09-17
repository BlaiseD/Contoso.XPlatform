using Contoso.Forms.Configuration.DetailForm;
using System.Globalization;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class TextFieldReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public TextFieldReadOnlyObject(string name, DetailControlSettingsDescriptor setting) : base(name, setting.TextTemplate.TemplateName)
        {
            DetailControlSettingsDescriptor = setting;
        }

        public DetailControlSettingsDescriptor DetailControlSettingsDescriptor { get; }

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrEmpty(DetailControlSettingsDescriptor.StringFormat))
                    return Value.ToString();

                return string.Format(CultureInfo.CurrentCulture, DetailControlSettingsDescriptor.StringFormat, Value);
            }
        }
    }
}
