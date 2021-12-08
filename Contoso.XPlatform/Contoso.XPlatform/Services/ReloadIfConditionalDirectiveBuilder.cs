using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class ReloadIfConditionalDirectiveBuilder : IReloadIfConditionalDirectiveBuilder
    {
        private readonly IMapper mapper;

        public ReloadIfConditionalDirectiveBuilder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ReloadIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties)
            => new ReloadIfConditionalDirectiveHelper<TModel>
            (
                formGroupSettings,
                properties,
                mapper
            ).GetConditions();
    }
}
