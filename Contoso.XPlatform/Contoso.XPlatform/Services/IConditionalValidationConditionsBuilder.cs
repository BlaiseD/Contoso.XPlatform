using Contoso.Forms.Configuration.Directives;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IConditionalValidationConditionsBuilder
    {
        List<ValidateIf<TModel>> GetConditions<TModel>(Dictionary<string, List<DirectiveDescriptor>> conditionalDirectives, IEnumerable<IValidatable> properties);
    }
}
