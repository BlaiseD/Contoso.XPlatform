using Contoso.Forms.Configuration.EditForm;

namespace Contoso.Forms.Configuration.DetailForm
{
    public interface IChildDetailGroupSettings : IDetailGroupSettings
    {
        string Placeholder { get; }
        FormGroupTemplateDescriptor FormGroupTemplate { get; }
    }
}
