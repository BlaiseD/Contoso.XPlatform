using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.Services
{
    public interface IReadOnlyFieldsCollectionBuilder
    {
        DetailFormLayout CreateFieldsCollection(IFormGroupSettings formSettings);
    }
}
