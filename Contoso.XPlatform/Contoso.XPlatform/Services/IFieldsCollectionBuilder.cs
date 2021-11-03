using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.Services
{
    public interface IFieldsCollectionBuilder
    {
        EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings);
    }
}
