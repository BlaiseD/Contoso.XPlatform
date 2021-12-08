﻿using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Utils
{
    public class HideIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<HideIf<TModel>, TModel>
    {
        public HideIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IValidatable> properties,
                                                 IMapper mapper,
                                                 List<HideIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
