using Contoso.Bsl.Configuration.ExpressionDescriptors;

namespace Contoso.Bsl.Business.Requests
{
    public class GetDropDownListRequest
    {
        public SelectorLambdaOperatorDescriptor Selector { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
    }
}
