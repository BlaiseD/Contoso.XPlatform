namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class InOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor ItemToFind { get; set; }
		public IExpressionOperatorDescriptor ListToSearch { get; set; }
    }
}