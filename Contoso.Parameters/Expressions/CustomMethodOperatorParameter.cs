using System.Reflection;

namespace Contoso.Parameters.Expressions
{
    public class CustomMethodOperatorParameter : IExpressionParameter
    {
		public CustomMethodOperatorParameter()
		{
		}

		public CustomMethodOperatorParameter(MethodInfo methodInfo, IExpressionParameter[] args)
		{
			MethodInfo = methodInfo;
			Args = args;
		}

		public MethodInfo MethodInfo { get; set; }
		public IExpressionParameter[] Args { get; set; }
    }
}