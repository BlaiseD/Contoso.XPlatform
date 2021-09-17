using Contoso.Forms.Configuration.EditForm;

namespace Contoso.Forms.Configuration.DetailForm
{
    public interface IChildDetailGroupSettings : IDetailGroupSettings
    {
        FormGroupTemplateDescriptor FormGroupTemplate { get; }
    }
}
