using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public interface IFieldsCollectionBuilder
    {
        void CreateFieldsCollection(IFormGroupSettings formSettings, ObservableCollection<IValidatable> properties);
    }
}
