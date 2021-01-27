using Contoso.Parameters.Expressions;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemFilterParameters
    {
        public SelectExpandItemFilterParameters()
        {
        }

        public SelectExpandItemFilterParameters(IExpressionParameter filterBody, string parameterName)
        {
            FilterBody = filterBody;
            ParameterName = parameterName;
        }

        public IExpressionParameter FilterBody { get; set; }
        public string ParameterName { get; set; }
    }
}
