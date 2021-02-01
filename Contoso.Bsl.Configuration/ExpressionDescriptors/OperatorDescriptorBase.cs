namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public abstract class OperatorDescriptorBase : IExpressionOperatorDescriptor
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
