using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public class UpdateOnlyFieldsCollectionBuilder : IUpdateOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public UpdateOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public ObservableCollection<IValidatable> CreateFieldsCollection(IFormGroupSettings formSettings)
            => new UpdateOnlyFieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
