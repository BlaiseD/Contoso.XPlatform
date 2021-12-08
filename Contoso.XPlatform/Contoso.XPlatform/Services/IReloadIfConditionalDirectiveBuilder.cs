using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IReloadIfConditionalDirectiveBuilder
    {
        List<ReloadIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties);
    }
}
