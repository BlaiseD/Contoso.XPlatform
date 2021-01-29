using Contoso.Bsl.Configuration.ExpressionDescriptors;

namespace Contoso.Bsl.Configuration.ExpansionDescriptors
{
    public class SelectExpandItemFilterDescriptor
    {
        public IExpressionOperatorDescriptor FilterBody { get; set; }
        public string ParameterName { get; set; }
    }
}
