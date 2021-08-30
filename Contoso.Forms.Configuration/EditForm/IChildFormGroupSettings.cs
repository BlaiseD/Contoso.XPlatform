namespace Contoso.Forms.Configuration.EditForm
{
    public interface IChildFormGroupSettings : IFormGroupSettings
    {
        FormGroupTemplateDescriptor FormGroupTemplate { get; }
    }
}
