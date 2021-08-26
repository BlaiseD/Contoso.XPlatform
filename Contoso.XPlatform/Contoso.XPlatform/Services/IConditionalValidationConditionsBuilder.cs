using Contoso.Forms.Configuration.Directives;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IConditionalValidationConditionsBuilder
    {
        List<ValidateIf<TModel>> GetConditions<TModel>(VariableDirectivesDictionaryDescriptor conditionalDirectives, IEnumerable<IValidatable> properties);
    }
}
