using Contoso.Parameters.Expressions;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemQueryFunctionParameters
    {
        public SelectExpandItemQueryFunctionParameters()
        {
        }

        public SelectExpandItemQueryFunctionParameters(IExpressionParameter methodCallDescriptor)
        {
            MethodCallDescriptor = methodCallDescriptor;
        }

        public IExpressionParameter MethodCallDescriptor { get; set; }
    }
}
