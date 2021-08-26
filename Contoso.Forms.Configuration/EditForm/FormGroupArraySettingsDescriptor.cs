using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class FormGroupArraySettingsDescriptor : FormItemSettingsDescriptor, IFormGroupSettings
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormGroupArray;
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public bool ShowTitle { get; set; }
        public string ModelType { get; set; }//e.g. T
        public string Type { get; set; }//e.g. ICollection<T>
        public string ValidFormControlText { get; set; }
        public string InvalidFormControlText { get; set; }
        public List<string> KeyFields { get; set; }
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public ValidationMessageDictionaryDescriptor ValidationMessages { get; set; }
        public VariableDirectivesDictionaryDescriptor ConditionalDirectives { get; set; }
    }
}
