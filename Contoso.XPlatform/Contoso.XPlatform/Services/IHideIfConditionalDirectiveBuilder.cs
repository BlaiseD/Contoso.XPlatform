using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IHideIfConditionalDirectiveBuilder
    {
        List<HideIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties);
    }
}
