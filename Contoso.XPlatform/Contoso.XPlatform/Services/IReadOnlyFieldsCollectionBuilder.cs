using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.Services
{
    public interface IReadOnlyFieldsCollectionBuilder
    {
        DetailFormLayout CreateFieldsCollection(IDetailGroupSettings formSettings);
    }
}
