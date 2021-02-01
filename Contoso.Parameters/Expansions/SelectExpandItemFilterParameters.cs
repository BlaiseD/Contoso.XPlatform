using Contoso.Parameters.Expressions;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemFilterParameters
    {
        public SelectExpandItemFilterParameters()
        {
        }

        public SelectExpandItemFilterParameters(FilterLambdaOperatorParameter filterLambdaOperator, string parameterName)
        {
            FilterLambdaOperator = filterLambdaOperator;
            ParameterName = parameterName;
        }

        public FilterLambdaOperatorParameter FilterLambdaOperator { get; set; }
        public string ParameterName { get; set; }
    }
}
