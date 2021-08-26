namespace Contoso.Forms.Configuration.Directives
{
    public class DirectiveDefinitionDescriptor
    {
        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public DirectiveArgumentDictionaryDescriptor Arguments { get; set; }
    }
}
