using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Utils
{
    public class ReloadIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<ReloadIf<TModel>, TModel>
    {
        public ReloadIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IValidatable> properties,
                                                 IMapper mapper,
                                                 List<ReloadIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
