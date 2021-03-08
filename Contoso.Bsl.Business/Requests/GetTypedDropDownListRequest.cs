using Contoso.Bsl.Configuration.ExpressionDescriptors;
using LogicBuilder.Data;
using LogicBuilder.Domain;

namespace Contoso.Bsl.Business.Requests
{
    public class GetTypedDropDownListRequest
    {
        public SelectorLambdaOperatorDescriptor Selector { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string ModelReturnType { get; set; }
        public string DataReturnType { get; set; }
    }
}
