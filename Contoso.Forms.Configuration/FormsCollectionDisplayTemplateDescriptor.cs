using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.Bindings;

namespace Contoso.Forms.Configuration
{
    public class FormsCollectionDisplayTemplateDescriptor
    {
        public string TemplateName { get; set; }
        public string PlaceHolderText { get; set; }
        public string ModelType { get; set; }
        public string LoadingIndicatorText { get; set; }
        public CollectionViewItemBindingsDictionary Bindings { get; set; }
        public SelectorLambdaOperatorDescriptor CollectionSelector { get; set; }
        public RequestDetailsDescriptor RequestDetails { get; set; }
    }
}
