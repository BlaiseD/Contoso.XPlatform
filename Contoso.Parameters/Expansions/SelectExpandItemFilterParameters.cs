using Contoso.Parameters.Expressions;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemFilterParameters
    {
        public SelectExpandItemFilterParameters()
        {
        }

        public SelectExpandItemFilterParameters(FilterLambdaOperatorParameter filterLambdaOperator)
        {
            FilterLambdaOperator = filterLambdaOperator;
        }

        public FilterLambdaOperatorParameter FilterLambdaOperator { get; set; }
    }
}
