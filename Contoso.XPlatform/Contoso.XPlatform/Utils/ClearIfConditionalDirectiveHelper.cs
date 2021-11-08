using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Utils
{
    public class ClearIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<ClearIf<TModel>, TModel>
    {
        public ClearIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IValidatable> properties,
                                                 IMapper mapper,
                                                 List<ClearIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
