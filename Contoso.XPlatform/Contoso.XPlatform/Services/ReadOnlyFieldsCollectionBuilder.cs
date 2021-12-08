using Contoso.Forms.Configuration.DataForm;
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

        public DetailFormLayout CreateFieldsCollection(IFormGroupSettings formSettings)
            => new ReadOnlyFieldsCollectionHelper(formSettings.FieldSettings, formSettings, this.contextProvider).CreateFields();
    }
}
