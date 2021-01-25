namespace Contoso.Parameters.Expressions
{
    public class ModuloBinaryOperatorParameter : BinaryOperatorParameter
    {
		public ModuloBinaryOperatorParameter()
		{
		}

		public ModuloBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}