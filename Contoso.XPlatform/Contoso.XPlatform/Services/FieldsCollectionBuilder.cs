using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public FieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings) 
            => new FieldsCollectionHelper(formSettings, this.contextProvider).CreateFields();
    }
}
