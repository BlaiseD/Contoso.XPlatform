using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        private readonly UiNotificationService uiNotificationService;
        private readonly IHttpService httpService;
        private readonly IMapper mapper;

        public FieldsCollectionBuilder(UiNotificationService uiNotificationService, IHttpService httpService, IMapper mapper)
        {
            this.uiNotificationService = uiNotificationService;
            this.httpService = httpService;
            this.mapper = mapper;
        }

        public ObservableCollection<IValidatable> CreateFieldsCollection(IFormGroupSettings formSettings) 
            => new FieldsCollectionHelper(formSettings, this.uiNotificationService, this.httpService, this.mapper, this).CreateFields();
    }
}
