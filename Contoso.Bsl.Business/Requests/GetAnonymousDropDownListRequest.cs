using Contoso.Common.Configuration.ExpressionDescriptors;
using LogicBuilder.Data;
using LogicBuilder.Domain;

namespace Contoso.Bsl.Business.Requests
{
    public class GetAnonymousDropDownListRequest
    {
        public SelectorLambdaOperatorDescriptor Selector { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
    }
}
