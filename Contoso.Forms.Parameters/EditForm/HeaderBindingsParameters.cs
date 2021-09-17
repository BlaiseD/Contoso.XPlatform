using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class HeaderBindingsParameters
    {
        public HeaderBindingsParameters
        (
            [Comments("Specify a format for the multi binding e.g. 'Value: {0:F2} {1}'")]
            [NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
            string headerStringFormat,

            [Comments("The list of fields to be bound in the multibinding.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
            List<string> fields,

            [Comments("Optional sub title string format.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
            string subTitleStringFormat = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string fieldTypeSource = null
        )
        {
            HeaderStringFormat = headerStringFormat;
            SubTitleStringFormat = subTitleStringFormat;
            Fields = fields;
        }

        public string HeaderStringFormat { get; set; }
        public string SubTitleStringFormat { get; set; }
        public List<string> Fields { get; set; }
    }
}
