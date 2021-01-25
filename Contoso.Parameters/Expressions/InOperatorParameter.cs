namespace Contoso.Parameters.Expressions
{
    public class InOperatorParameter : IExpressionParameter
    {
		public InOperatorParameter()
		{
		}

		public InOperatorParameter(IExpressionParameter itemToFind, IExpressionParameter listToSearch)
		{
			ItemToFind = itemToFind;
			ListToSearch = listToSearch;
		}

		public IExpressionParameter ItemToFind { get; set; }
		public IExpressionParameter ListToSearch { get; set; }
    }
}