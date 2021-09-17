using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public class ReadOnlyFieldsCollectionBuilder : IReadOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public ReadOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public ObservableCollection<IReadOnly> CreateFieldsCollection(IDetailGroupSettings formSettings)
            => new ReadOnlyFieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
