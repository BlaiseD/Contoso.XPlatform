using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.Services
{
    public class ReadOnlyFieldsCollectionBuilder : IReadOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public ReadOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public DetailFormLayout CreateFieldsCollection(IDetailGroupSettings formSettings)
            => new ReadOnlyFieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
