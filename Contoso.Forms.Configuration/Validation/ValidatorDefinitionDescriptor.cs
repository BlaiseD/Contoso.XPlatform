namespace Contoso.Forms.Configuration.Validation
{
    public class ValidatorDefinitionDescriptor
    {
        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public ValidatorArgumentDictionaryDescriptor Arguments { get; set; }
    }
}
