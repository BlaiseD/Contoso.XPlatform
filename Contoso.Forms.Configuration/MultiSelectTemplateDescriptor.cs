using Contoso.Bsl.Configuration.ExpressionDescriptors;

namespace Contoso.Forms.Configuration
{
    public class MultiSelectTemplateDescriptor
    {
        public string TemplateName { get; set; }
        public string PlaceHolderText { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public string ModelType { get; set; }
        public string TextAndValueObjectType { get; set; }
        public SelectorLambdaOperatorDescriptor TextAndValueSelector { get; set; }
    }
}
