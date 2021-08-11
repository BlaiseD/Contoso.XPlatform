using Contoso.Forms.Configuration.Validation;

namespace Contoso.Forms.Configuration.EditForm
{
    public class FormControlSettingsDescriptor : FormItemSettingsDescriptor
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormControl;
        public string DomElementId { get; set; }
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public string StringFormat { get; set; }
        public string Type { get; set; }
        public FieldValidationSettingsDescriptor ValidationSetting { get; set; }
        public TextFieldTemplateDescriptor TextTemplate { get; set; }
        public DropDownTemplateDescriptor DropDownTemplate { get; set; }
    }
}
