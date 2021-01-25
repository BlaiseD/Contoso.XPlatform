namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class InOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor ItemToFind { get; set; }
		public IExpressionDescriptor ListToSearch { get; set; }
    }
}