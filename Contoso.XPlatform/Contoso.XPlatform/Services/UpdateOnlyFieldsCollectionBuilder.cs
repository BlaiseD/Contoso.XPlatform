using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.Services
{
    public class UpdateOnlyFieldsCollectionBuilder : IUpdateOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public UpdateOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings)
            => new UpdateOnlyFieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
