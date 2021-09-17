using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public interface IReadOnlyFieldsCollectionBuilder
    {
        ObservableCollection<IReadOnly> CreateFieldsCollection(IDetailGroupSettings formSettings);
    }
}
