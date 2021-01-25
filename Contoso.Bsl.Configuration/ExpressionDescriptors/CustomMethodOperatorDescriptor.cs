using System.Reflection;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CustomMethodOperatorDescriptor : IExpressionDescriptor
    {
		public MethodInfo MethodInfo { get; set; }
		public IExpressionDescriptor[] Args { get; set; }
    }
}