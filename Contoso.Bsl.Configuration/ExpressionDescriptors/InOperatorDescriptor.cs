namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class InOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor ItemToFind { get; set; }
		public IExpressionOperatorDescriptor ListToSearch { get; set; }
    }
}