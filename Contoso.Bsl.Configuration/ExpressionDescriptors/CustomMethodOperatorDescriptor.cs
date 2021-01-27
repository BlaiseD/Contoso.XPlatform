using System.Reflection;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CustomMethodOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public MethodInfo MethodInfo { get; set; }
		public IExpressionOperatorDescriptor[] Args { get; set; }
    }
}